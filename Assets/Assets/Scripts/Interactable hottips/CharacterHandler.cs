using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public string npcName;
    public DialogueContainer myDialogue;
    public Camera myCamera;
    private void Awake()
    {
        myCamera.enabled = false;
    }
    public DialogueContainer GetMyDialogue()
    {
        return myDialogue;
    }
    public Camera GetMyCamera()
    {
        return myCamera;
    }

}
