using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueContainer : ScriptableObject
{
    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    public List<DialogueNodeData> DialogueNodeData = new List<DialogueNodeData>();
    public Dictionary<string, DialogueNodeData> NodeLibrary;
    //MAYBE BECAUSE AWAKE DOESNT PLAY ON THIS
    public void CreateLibrary()
    {
        NodeLibrary = new Dictionary<string, DialogueNodeData>();
        foreach (DialogueNodeData n in DialogueNodeData)
        {
            NodeLibrary.Add(n.NodeGUID, n);
        }
    }
}
