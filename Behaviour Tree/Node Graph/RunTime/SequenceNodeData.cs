using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNodeData : BehaviourNodeData
{
    public override BehaviourNode BuildNode()
    {
        Debug.Log("SelectorNode");
        var newNode = new SequenceNode()
        {
            NodeId = this.NodeGuid,
            ReturnPolicy = this.ReturnPolicy,
            RepeatPolicy = this.RepeatPolicy
        };
        return newNode;
    }
}
