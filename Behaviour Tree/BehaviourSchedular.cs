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
        Root = this.BuildTree(TreeData);
        if(Root != null)
        this.ActiveBehaviours.Enqueue(Root);

    }
    private BehaviourNode BuildTree(BehaviourContainer treeData)
    {
       
        var NodeList = new Dictionary<string,BehaviourNode>();
        foreach (var nodeData in treeData.BehaviourNodeData)
        {
            Debug.Log("BUILD Tree");
            var newNode = nodeData.BuildNode();
          
            NodeList[newNode.NodeId]=newNode;  
        }
        foreach (var linkData in treeData.NodeLinks)
        {
            if (linkData != treeData.NodeLinks[0])
            {
                var parentNode = NodeList[linkData.BaseNodeGuid];
                var childNode = NodeList[linkData.TargetNodeGuid];
                parentNode.Children.Add(childNode);
            }
        }
        return NodeList[treeData.NodeLinks[0].TargetNodeGuid]; 
    }
    bool Step()
    {
        BehaviourNode currentNode = this.ActiveBehaviours.Dequeue();
        if (currentNode == null) return false;
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
        while (Step()) { }//?
    }
    private void Update()
    {
        if (this.ActiveBehaviours.Count > 0)
        {
            Tick();
        }
        else
        {
          this.ActiveBehaviours.Enqueue(this.Root);
        }
    }
}
