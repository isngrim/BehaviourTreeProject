using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ParallelNodeData : BehaviourNodeData
{
    public Policy SuccessPolicy = Policy.REQUIRE_ALL;

    public override BehaviourNode BuildNode()
    {
        Debug.Log("ParallelNode");
        var newNode = new ParallelNode()
        {
            NodeId = this.NodeGuid,
            ReturnPolicy = this.ReturnPolicy,
            RepeatPolicy = this.RepeatPolicy,
            SuccessPolicy = this.SuccessPolicy
        };
        return newNode;
    }
}
