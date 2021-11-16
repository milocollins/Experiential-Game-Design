using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

//Attach to ui.dialoguePanel's parent

public class DialogueManager : MonoBehaviour
{
    #region Variables
    public static DialogueManager manager;
    public bool dialogueActive = false;
    public DialogueGraph graph;
    private CharacterProfile detectiveProfile;
    private CharacterProfile currentCharacter;
    //UI
    public Camera currentCamera;
    public Text nameField;
    public Text dialogueField;
    #endregion
    private void Awake()
    {
        manager = this;
    }
    public void Start()
    {
        
    }
    public void StartDialogue(Transform t)
    {
        dialogueActive = true;
        currentCharacter = t.GetComponent<CharacterProfile>();
        //Cameras
        //NPC Behaviour
        //UI On
        UIManager.ui.ToggleDialoguePanel();
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
        //Camera
        //Set Text options
        //Play Animation & SFX
        //Wait For Input to Procceed
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
        //Procceed to next node
        //Remove SFX & Animation in case still running
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
            SetupDialogueOptions();
        }
    }
    //Shows dialogue options
    private void SetupDialogueOptions()
    {

    }
    //Selects dialogue option
    public void OnButton_SelectDialogue()
    {
        graph.currentNode = 
        //Choose Option, then procceed to next node
        NextNode();

    }
    public void EndDialogue()
    {
        dialogueActive = false;
        //UI Off
        //Camera Return
        //NPC Behaviour
    }
}
