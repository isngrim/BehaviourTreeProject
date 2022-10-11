using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class BehaviourSchedular : MonoBehaviour
{
    Queue<BehaviourNode> ActiveBehaviours;
    [SerializeReference]
   public BehaviourNode Root;
    public BehaviourContainer TreeData;
    IBlackboard Blackboard;

    private void Start()
    {
        Blackboard = GetComponent<CharacterBlackboard>();
        this.ActiveBehaviours = new Queue<BehaviourNode>();
        this.Root = this.BuildTree(this.TreeData);

      Debug.Log("root generated as " + this.Root);
    
            //add root to queue
            if (Root != null)
                this.ActiveBehaviours.Enqueue(this.Root);

    }
    //build tree from node data
    private BehaviourNode BuildTree(BehaviourContainer treeData)
    {
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
        //grab first node in queue
        var currentNode = this.ActiveBehaviours.Dequeue();
        if (currentNode == null) { return false; }
        //initialize it
        currentNode.OnInitialize();
        //update the node
        List<BehaviourNode> returnedNodes = currentNode.UpdateNode(Blackboard);
        //enqueue its children
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
        //add end of queue marker
        this.ActiveBehaviours.Enqueue(null);
        while (Step()) { /*for logging */}
    }
    private void Update()
    {
        if (this.ActiveBehaviours.Count > 0)
        {
           // Debug.Log("Schedular is ticking");
            Tick();
        }
        else
        {
          this.ActiveBehaviours.Enqueue(this.Root);
        }
    }
}
