using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNode : MonoBehaviour
{
    private DialogueNodeData storedNode;
    public DialogueNodeData GetNode()
    {
        if (storedNode != null)
        {
            return storedNode;
        }
        return null;
    }
    public void SetNode(DialogueNodeData n)
    {
        storedNode = n;
    }
}
