using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphSaveUtility 
{
    private DialogueGraphView TargetGraphView;
    private List<Edge> edges => TargetGraphView.edges.ToList();
    private List<DialogueNode> nodes => TargetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();
    private DialogueContainer containerCache;

    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            TargetGraphView = targetGraphView
        };
    }
    public void SaveGraph(string fileName)
    {
        if (!edges.Any())
        {
            return;
        }
        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
        //var connectedPorts = new List<Edge>();
        var connectedPorts = edges.Where(x => x.input.node != null).ToArray();
        //foreach (var edge in edges)
        //{
        //    if (edge.input.node != null)
        //    {
        //        connectedPorts.Add(edge);
        //    }
        //}
        for (int i = 0; i < connectedPorts.ToArray().Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;

            dialogueContainer.NodeLinks.Add(item: new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            });
        }
        foreach (var dialogueNode in nodes)
        {
            DialogueNodeData n = new DialogueNodeData
            {
                NodeGUID = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogueText,
                Position = dialogueNode.GetPosition().position,
                EntryPoint = dialogueNode.entryPoint
            };
            dialogueContainer.DialogueNodeData.Add(n);
        }
        AssetDatabase.CreateAsset(dialogueContainer, path: $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }
    public void LoadGraph(string fileName)
    {
        containerCache = Resources.Load<DialogueContainer>(fileName);
        if (containerCache == null)
        {
            EditorUtility.DisplayDialog("File Not Found", "Target dialogue graph file does not exist!", "OK");
            return;
        }
        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }
    private void ClearGraph()
    {
        nodes.Find(match: x => x.entryPoint).GUID = containerCache.NodeLinks[0].BaseNodeGuid;
        foreach (var node in nodes)
        {
            if (node.entryPoint)
            {
                continue;
            }
            edges.Where(x  => x.input.node == node).ToList().ForEach(edge => TargetGraphView.RemoveElement(edge));
            TargetGraphView.RemoveElement(node);
        }
    }

    private void CreateNodes()
    {
        foreach (var nodeData in containerCache.DialogueNodeData)
        {
            var tempNode = TargetGraphView.CreateDialogueNode(nodeData.DialogueText);
            tempNode.GUID = nodeData.NodeGUID;
            TargetGraphView.AddElement(tempNode);

            var nodePorts = containerCache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.NodeGUID).ToList();
            nodePorts.ForEach(x => TargetGraphView.AddChoicePort(tempNode, x.PortName));
        }
    }

    private void ConnectNodes()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            var connections = containerCache.NodeLinks.Where(x => x.BaseNodeGuid == nodes[i].GUID).ToList();
            for (int j = 0; j < connections.Count; j++)
            {
                var targetNodeGuide = connections[j].TargetNodeGuid;
                var targetNode = nodes.First(x => x.GUID == targetNodeGuide);
                LinkNodes( nodes[i].outputContainer.Q<Port>(), (Port) targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(
                    containerCache.DialogueNodeData.First(x => x.NodeGUID == targetNodeGuide).Position,
                    TargetGraphView.defaultNodeSize
                ));
            }
        }
    }

    private void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge
        {
            output = output,
            input = input
        };
        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);
        TargetGraphView.Add(tempEdge);
    }
}
