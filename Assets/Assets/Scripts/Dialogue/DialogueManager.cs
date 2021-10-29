using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager manager;
    public bool dialogueActive = false;
    private DialogueContainer currentDialogue;
    private DialogueNodeData currentDialogueNode;
    private Camera npcCamera;
    public Camera firstPerson;
    public Camera thirdPerson;
    private int optionsInt;
    private void Awake()
    {
        manager = this;
        thirdPerson.enabled = false;
        dialogueActive = false;
    }
    private void Update()
    {
        if (dialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                LeaveDialogue();
            }
        }
    }
    public void GetDialogueOptions()
    {
        List<DialogueNodeData> nextNodes = new List<DialogueNodeData>();
        //THIS ADDS ALL THE NODES CONNECTED TO THE BASE NODE TO THIS LIST
        foreach (NodeLinkData n in currentDialogue.NodeLinks)
        {
            if (n.BaseNodeGuid == currentDialogueNode.NodeGUID)
            {
                Debug.Log("Found Target Node");
                Debug.Log(n.TargetNodeGuid);
                Debug.Log(currentDialogue.NodeLibrary[n.TargetNodeGuid]);
                nextNodes.Add(currentDialogue.NodeLibrary[n.TargetNodeGuid]);
            }
        }
        if (nextNodes.Count == 0)
        {
            //if baseguid is still traversable, go back
            //else
            Debug.Log("End Of Dialogue Tree");
        }
        else if (nextNodes.Count == 1)
        {
            currentDialogueNode = currentDialogue.NodeLibrary[nextNodes[0].NodeGUID];
            StartCoroutine(PlayDialogue(currentDialogueNode));
        }
        else
        {
            SetDialogueOptions(nextNodes);
        }
    }
    public void GetDialogueOptions(DialogueNodeData n)
    {
        GetDialogueOptions();
    }
    public void SetDialogueOptions(List<DialogueNodeData> buttonNodes)
    {
        foreach (GameObject n in UIManager.ui.dialogueButtons)
        {
            n.GetComponent<ButtonNode>().SetNode(null);
        }
        for (int i = 0; i < UIManager.ui.dialogueButtons.Count; i++)
        {
            if (i < buttonNodes.Count)
            {
                UIManager.ui.dialogueButtons[i].GetComponentInChildren<Text>().text = buttonNodes[i].DialogueText;
                UIManager.ui.dialogueButtons[i].GetComponent<ButtonNode>().SetNode(buttonNodes[i]);
                UIManager.ui.dialogueButtons[i].SetActive(true);
            }
        }
    }
    public void OnButton_ChooseDialogue(GameObject go)
    {
        StartCoroutine(PlayDialogue(go.GetComponent<ButtonNode>().GetNode()));
        for (int i = 0; i < UIManager.ui.dialogueButtons.Count; i++)
        {
            UIManager.ui.dialogueButtons[i].SetActive(false);
            go.GetComponent<ButtonNode>().SetNode(null);
        }
    }
    public IEnumerator PlayDialogue(DialogueNodeData n)
    {
        currentDialogueNode = n;
        //Currently empty at start
        UIManager.ui.SetDialogueSubtitle(currentDialogueNode.DialogueText);
        yield return new WaitForSeconds(2f);
        //WAIT for n.dialogue audioclip length
        GetDialogueOptions(n);
    }
    public void StartDialogue(Transform t)
    {
        dialogueActive = true;
        CharacterHandler c = t.GetComponent<CharacterHandler>();
        currentDialogue = c.GetMyDialogue();
        currentDialogue.CreateLibrary();
        Debug.Log(currentDialogue + " HIT");
        npcCamera = c.GetMyCamera();
        CameraController.firstPerson.ToggleInput();
        PlayerController.thePlayer.ToggleInput();
        firstPerson.enabled = !firstPerson.enabled;
        thirdPerson.enabled = !thirdPerson.enabled;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        UIManager.ui.ToggleDialoguePanel();
        //
        // This Returns empty
        for (int i = 0; i < currentDialogue.DialogueNodeData.Count; i++)
        {
            if (currentDialogue.DialogueNodeData[i].EntryPoint)
            {
                Debug.Log("FOUND IT");
                currentDialogueNode = currentDialogue.DialogueNodeData[i];
                break;
            }
        }
        //
        StartCoroutine(PlayDialogue(currentDialogueNode));
    }
    public void LeaveDialogue()
    {
        dialogueActive = false;
        CameraController.firstPerson.ToggleInput();
        PlayerController.thePlayer.ToggleInput();
        UIManager.ui.ToggleDialoguePanel();
        firstPerson.enabled = !firstPerson.enabled;
        thirdPerson.enabled = !thirdPerson.enabled;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
