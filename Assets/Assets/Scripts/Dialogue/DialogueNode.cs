using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueNode : Node
{
    public string GUID;
    public string DialogueText;
    public bool entryPoint = false;
    public Motive motive;
}
