using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SelectorNodeData : BehaviourNodeData
{
    public override BehaviourNode BuildNode()
    {
        var newNode = new SelectorNode()
        {
            NodeId = this.NodeGuid,
            ReturnPolicy = this.ReturnPolicy,
            RepeatPolicy = this.RepeatPolicy
        };
        return newNode;
    }
}
