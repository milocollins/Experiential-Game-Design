using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNodeData 
{
    public string NodeGUID;
    public bool isLocked = false;
    public bool traversable = true;
    public string DialogueText;
    public bool EntryPoint = false;
    public Vector2 Position;
}
