using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelNodeData : BehaviourNodeData
{
    public override BehaviourNode BuildNode()
    {
        BehaviourNode newNode = null;
        Debug.Log("ParallelNode");
        newNode = new ParallelNode
        {
            Log = this.NodeType.ToString(),
            NodeId = this.NodeGuid,
            ReturnPolicy = this.ReturnPolicy,
            RepeatPolicy = this.RepeatPolicy,
            SuccessPolicy = this.SuccessPolicy
        };
        return newNode;
       
    }

}