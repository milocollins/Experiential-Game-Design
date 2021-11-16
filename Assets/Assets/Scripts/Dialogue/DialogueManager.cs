using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;


public class DialogueManager : MonoBehaviour
{
    #region Variables
    public static DialogueManager manager;
    public bool dialogueActive = false;
    public DialogueGraph graph;
    private CharacterProfile detectiveProfile;
    private CharacterHandler currentCharacter;
    private BaseNode[] optionNodes = new BaseNode[4];
    private GameObject canvas;
    private Camera mainCam;
    //UI
    public Camera currentCamera;
    public Text nameField;
    public Text dialogueField;
    public GameObject[] dialogueOptions = new GameObject[4];
    public Color defaultColour;
    public Color lockedColour;
    #endregion
    private void Awake()
    {
        manager = this;
        for (int i = 0; i < dialogueOptions.Length; i++)
        {
            string s = "DialogueOption" + i;
            dialogueOptions[i] = GameObject.Find(s);
        }
        canvas = transform.GetChild(0).gameObject;
        nameField = GameObject.Find("Dialogue").transform.GetChild(0).GetComponent<Text>();
        dialogueField = GameObject.Find("Dialogue").transform.GetChild(1).GetComponent<Text>();
        detectiveProfile = PlayerController.thePlayer.myProfile;
    }
    public void Start()
    {
        canvas.SetActive(false);
    }
    public void StartDialogue(Transform t)
    {
        dialogueActive = true;
        currentCharacter = t.GetComponent<CharacterHandler>();
        graph = currentCharacter.dialogue;
        //Cameras
        mainCam = Camera.main;
        Camera.main.enabled = false;
        PlayerController.thePlayer.dialogueCamera.enabled = true;
        //NPC Behaviour
        //UI On
        CameraController.firstPerson.ToggleInput();
        PlayerController.thePlayer.ToggleInput();
        canvas.SetActive(dialogueActive);
        canvas.SetActive(true);
        foreach (BaseNode b in graph.nodes)
        {
            if (b.ReturnString() == "Start")
            {
                graph.currentNode = b;
                break;
            }
        }
        NextNode();
    }
    private IEnumerator PlayDialogue()
    {
        //Set Text options
        string s = graph.currentNode.ReturnString();
        string[] sParts = s.Split('/');
        nameField.text = sParts[0];
        dialogueField.text = sParts[1];
        //Camera
        if (nameField.text == detectiveProfile.name)
        {
            PlayerController.thePlayer.dialogueCamera.enabled = true;
            currentCharacter.dialogueCamera.enabled = false;
        }
        else
        {
            PlayerController.thePlayer.dialogueCamera.enabled = false;
            currentCharacter.dialogueCamera.enabled = true;
        }
        //Play Animation & SFX
        //Wait For Input to Procceed
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
        //Remove SFX & Animation in case still running
        //Procceed to next node
        graph.currentNode.UnlockInformation();
        NextNode();
    }
    private void NextNode()
    {
        StopCoroutine(PlayDialogue());
        //Find Current Port
        NodePort exitPort = null;
        foreach (NodePort p in graph.currentNode.Ports)
        {
            if (p.fieldName == "exit")
            {
                exitPort = p;
                break;
            }
        }
        if (exitPort.ConnectionCount == 1)
        {
            graph.currentNode = exitPort.Connection.node as BaseNode;
            //Exit Node
            if (graph.currentNode.ReturnString() == "Exit")
            {
                EndDialogue();
            }
            //No Options
            else
            {
                StartCoroutine(PlayDialogue());
            }
        }
        //Shouldn't hit this
        else if (exitPort.ConnectionCount == 0)
        {
            Debug.Log("That ain't right Chief, dialogue tree unfinished");
            EndDialogue();
        }
        //Setup on Several dialogue options
        else
        {
            SetupDialogueOptions(exitPort);
        }
    }
    //Shows dialogue options
    private void SetupDialogueOptions(NodePort p)
    {
        for (int i = 0; i < p.ConnectionCount; i++)
        {
            BaseNode b = p.GetConnection(i).node as BaseNode;
            if (b.GetBool())
            {
                dialogueOptions[i].GetComponentInChildren<Text>().text = i+ ". " + "[REDACTED]";
                dialogueOptions[i].GetComponentInChildren<Text>().color = lockedColour;
            }
            else
            {
                dialogueOptions[i].GetComponentInChildren<Text>().text = i + ". " + b.ReturnString();
                dialogueOptions[i].GetComponentInChildren<Text>().color = defaultColour;
                WaitForInput(i, b);
            }
            dialogueOptions[i].SetActive(true);
        }
        dialogueOptions[p.ConnectionCount + 1].GetComponentInChildren<Text>().text = (p.ConnectionCount+1) +". " + "[Leave Dialogue]";
        dialogueOptions[p.ConnectionCount + 1].GetComponentInChildren<Text>().color = defaultColour;
        dialogueOptions[p.ConnectionCount + 1].SetActive(true);
        WaitForInput(p.ConnectionCount + 1);
    }
    //Selects dialogue option
    private void WaitForInput(int i, BaseNode b)
    {
        switch (i)
        {
            case 0:
                StartCoroutine(KeyListener(KeyCode.Alpha1, b));
                break;
            case 1:
                StartCoroutine(KeyListener(KeyCode.Alpha2, b));
                break;
            case 2:
                StartCoroutine(KeyListener(KeyCode.Alpha3, b));
                break;
            case 3:
                StartCoroutine(KeyListener(KeyCode.Alpha4, b));
                break;
            default:
                Debug.Log("Well that shouldn't happen");
                break;
        }
    }
    private void WaitForInput(int i)
    {
        switch (i)
        {
            case 0:
                StartCoroutine(KeyListener(KeyCode.Alpha1));
                break;
            case 1:
                StartCoroutine(KeyListener(KeyCode.Alpha2));
                break;
            case 2:
                StartCoroutine(KeyListener(KeyCode.Alpha3));
                break;
            case 3:
                StartCoroutine(KeyListener(KeyCode.Alpha4));
                break;
            default:
                Debug.Log("Well that shouldn't happen");
                break;
        }
    }
    //Waits for Input
    private IEnumerator KeyListener(KeyCode k, BaseNode b)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(k));
        graph.currentNode = b;
        StopAllCoroutines();
        StartCoroutine(PlayDialogue());
    }
    private IEnumerator KeyListener(KeyCode k)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(k));
        StopAllCoroutines();
        LeaveDialogue();
    }
    //Parting Comment Before Exiting Dialogue
    public void LeaveDialogue()
    {
        foreach (BaseNode b in graph.nodes)
        {
            if (b.ReturnString() == "Leave")
            {
                graph.currentNode = b;
                break;
            }
        }
        NextNode();
    }
    //What it says on the tin
    public void EndDialogue()
    {
        dialogueActive = false;
        //UI Off
        ToggleCanvas();
        CameraController.firstPerson.ToggleInput();
        PlayerController.thePlayer.ToggleInput();
        //Camera Return
        Camera.main.enabled = false;
        mainCam.enabled = true;
        //NPC Behaviour
    }
    private void ToggleCanvas()
    {
        canvas.SetActive(dialogueActive);
    }
}
