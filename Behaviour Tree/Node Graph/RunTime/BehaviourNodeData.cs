using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[Serializable]
public class BehaviourNodeData 
{
    public string NodeGuid;
    public Policy SuccessPolicy = Policy.REQUIRE_ALL;
    //probably need to replace this with Node data
    public string BehaviourStuff;
    public Vector2 NodePosition;
    public NodeType NodeType;
    public RepeatPolicies RepeatPolicy;
    public ReturnPolicies ReturnPolicy;
    public BehaviourNode BuildNode()
    {
         BehaviourNode newNode = null;
        if (this.NodeType == NodeType.PARALLEL)
        {
            Debug.Log("ParallelNode");
            newNode = new ParallelNode
            {
            Log = this.NodeType.ToString(),
            NodeId = this.NodeGuid,
            ReturnPolicy = this.ReturnPolicy,
            RepeatPolicy = this.RepeatPolicy,
            SuccessPolicy = this.SuccessPolicy
        };
 
        }
        if (this.NodeType == NodeType.SELECTOR)
        {
            Debug.Log("SelectorNode");
             newNode = new SelectorNode()
            {
                 Log = this.NodeType.ToString(),
                 NodeId = this.NodeGuid,
                ReturnPolicy = this.ReturnPolicy,
                RepeatPolicy = this.RepeatPolicy
             
            };
      
        }
        if (this.NodeType == NodeType.SEQUENCE)
        {
            Debug.Log("SEQUENCENode");
            newNode = new SequenceNode()
            {
                Log = this.NodeType.ToString(),
                NodeId = this.NodeGuid,
                ReturnPolicy = this.ReturnPolicy,
                RepeatPolicy = this.RepeatPolicy
            };
       
        }
     
        return newNode ;
    }
}
