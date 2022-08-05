using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BehaviourSchedular : MonoBehaviour
{
    Queue<BehaviourNode> ActiveBehaviours;
    [SerializeField]
   public BehaviourNode Root;
    public BehaviourContainer TreeData;
    //   Blackboard Blackboard;
    private void Start()
    {
        this.ActiveBehaviours = new Queue<BehaviourNode>();
        this.Root = this.BuildTree(this.TreeData);
        
        Debug.Log("root generated as " + this.Root.NodeId);
        // if(Root != null)
        this.ActiveBehaviours.Enqueue(this.Root);
        if(this.ActiveBehaviours.Count <=0) { Debug.Log("root enqueued"); }

    }
    private BehaviourNode BuildTree(BehaviourContainer treeData)
    {
        Debug.Log("BUILD Tree");
        var NodeList = new Dictionary<string,BehaviourNode>();
        foreach (var nodeData in treeData.BehaviourNodeData)
        {
            
            var newNode = nodeData.BuildNode();
            Debug.Log(newNode.NodeId);
            NodeList.Add(nodeData.NodeGuid, newNode);
 
        }
        foreach (var linkData in treeData.NodeLinks)
        {
            if (linkData.PortName != "Next")
            {
                
                var parentNode = NodeList[linkData.BaseNodeGuid];
                var childNode = NodeList[linkData.TargetNodeGuid];
                if(childNode !=null)
                parentNode.Children.Add(childNode);
                Debug.Log(parentNode.NodeId + " linked to " + childNode.NodeId);
            }
        }
        return NodeList[treeData.NodeLinks[0].TargetNodeGuid]; 
    }
    bool Step()
    {
        var currentNode = this.ActiveBehaviours.Dequeue();
        if (currentNode == null) { Debug.Log("no node"); return false; }
        currentNode.OnInitialize();
        List<BehaviourNode> returnedNodes = currentNode.UpdateNode();
        foreach(var node in returnedNodes)
        {
            if(node != null)
            this.ActiveBehaviours.Enqueue(node);
        }
        if (currentNode.IsTerminated())
        {
            //log completion or do anything else related to the completion of a node
        }
        else
        {
            this.ActiveBehaviours.Enqueue(currentNode);
        }
            return true;
    }
    protected void Tick()
    {
        this.ActiveBehaviours.Enqueue(null);
        while (Step()) { Debug.Log("Schedular is steping through tree"); }//?
    }
    private void Update()
    {
        if (this.ActiveBehaviours.Count > 0)
        {
            Debug.Log("Schedular is ticking");
            Tick();
        }
        else
        {
          this.ActiveBehaviours.Enqueue(this.Root);
        }
    }
}
