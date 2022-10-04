using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNodeData : BehaviourNodeData
{
    public override BehaviourNode BuildNode()
    {
        var newNode = 
        new SelectorNode()
        {
            Log = this.NodeType.ToString(),
            NodeId = this.NodeGuid,
            ReturnPolicy = this.ReturnPolicy,
            RepeatPolicy = this.RepeatPolicy

        };
        Debug.Log("SelectorNode");
        return newNode;
    }
}
