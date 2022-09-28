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
    private BehaviourGraphView TargetGraphView;
    private BehaviourContainer ContainerChache;
    private List<Edge> Edges => TargetGraphView.edges.ToList();
    private List<BehaviourGraphNode> Nodes => TargetGraphView.nodes.ToList().Cast<BehaviourGraphNode>().ToList();
    public static GraphSaveUtility GetInstance(BehaviourGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            TargetGraphView = targetGraphView
         };
    }
    public void SaveGraph(string fileName)
    {
       
        if (!this.Edges.Any()) return;
        var behaviourContainer = ScriptableObject.CreateInstance<BehaviourContainer>();

        var connectedPorts  = this.Edges.Where(x  => x.input.node != null).ToArray();
        for (var i = 0;i < connectedPorts.Count();i++)
        {
            BehaviourGraphNode outputNode = (connectedPorts[i].output.node as BehaviourGraphNode);
            BehaviourGraphNode inputNode = (connectedPorts[i].input.node as BehaviourGraphNode);
            behaviourContainer.NodeLinks.Add( new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID


            });

            foreach (var node in this.Nodes.Where(node => !node.EntryPoint))
            {
                bool isNewNode = true;
                foreach(var nodeData in behaviourContainer.BehaviourNodeData)
                {

                    if (nodeData.NodeGuid == node.GUID)
                        isNewNode = false;
                }
                if (isNewNode)
                {
                    var newData = node.SaveToNodeData();
                    behaviourContainer.BehaviourNodeData.Add(newData);
                    
                }
            }
          
        }
        if(!AssetDatabase.IsValidFolder(path: "Assets/Resources"))
        {
            AssetDatabase.CreateFolder(parentFolder: "Assets", newFolderName: "Resources");
        }
        if(AssetDatabase.Contains(behaviourContainer))
        {
            AssetDatabase.DeleteAsset(path: $"Assets/Resources/{fileName}.asset");
        }
        AssetDatabase.CreateAsset(behaviourContainer, path: $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }
    public void LoadGraph(string fileName)
    {
        
        this.ContainerChache = Resources.Load<BehaviourContainer>(fileName);
        if(this.ContainerChache == null)
        {
            EditorUtility.DisplayDialog(title: "File Not Found", message: "Target Behaviour Graph file does not exist", ok: "OK");
            return;
        }
        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }
    public BehaviourNode InstantiateTree(string fileName)
    {
        this.ContainerChache = Resources.Load<BehaviourContainer>(fileName);
        if (this.ContainerChache == null)
        {
            Debug.Log("Target Behaviour Graph file does not exist");
            return null;
        }
        BehaviourNode root = new SelectorNode() ;

        return root;
    }
    private void ConnectNodes()
    {
       for (var i=0; i< Nodes.Count;i++)
        {
            var k = i;
            var connections = this.ContainerChache.NodeLinks.Where(x => x.BaseNodeGuid == Nodes[k].GUID).ToList();
            for (var j = 0; j < connections.Count; j++)
            {
                var targetNodeGUID = connections[j].TargetNodeGuid;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGUID);
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(),(Port) targetNode.inputContainer[0]);
                targetNode.SetPosition(new Rect(
                    this.ContainerChache.BehaviourNodeData.First(x => x.NodeGuid == targetNodeGUID).NodePosition, 
                    this.TargetGraphView.DefaultNodeSize));
            }
        }
    }

    private void LinkNodes(Port _output, Port _input)
    {
        var tempEdge = new Edge
        {
            output = _output,
            input = _input
        };
        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);
        this.TargetGraphView.Add(tempEdge);
    }

    private void CreateNodes()
    {
       foreach(var nodeData in this.ContainerChache.BehaviourNodeData)
        {
            var tempNode = this.TargetGraphView.CreateBehaviourNode(nodeData);
            tempNode.GUID = nodeData.NodeGuid;
            TargetGraphView.AddElement(tempNode);
            var nodePorts = this.ContainerChache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.NodeGuid).ToList();
            nodePorts.ForEach(x => this.TargetGraphView.AddChoicePort(tempNode,x.PortName));
        }
    }

    private void ClearGraph()
    {
        //Sett Entry Point from save, Discards Existing GUID
        Nodes.Find(x => x.EntryPoint).GUID = this.ContainerChache.NodeLinks[0].BaseNodeGuid;
        foreach(var node in this.Nodes)
        {
            if (node.EntryPoint) continue;
            //remove the connections from the graph
            this.Edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge => this.TargetGraphView.RemoveElement(edge));
            //remove the node from the graph
            this.TargetGraphView.RemoveElement(node);
        }
    }
}
