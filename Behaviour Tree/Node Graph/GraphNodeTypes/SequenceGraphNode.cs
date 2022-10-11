using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SequenceGraphNode : BehaviourGraphNode
{
    public SequenceGraphNode()
    {
        this.nodeType = NodeType.SEQUENCE;
    }
    public SequenceGraphNode( BehaviourNodeData nodeData)
    {
        this.nodeType = nodeData.NodeType;
        this.RepeatPolicy = nodeData.RepeatPolicy;
    }
    public override BehaviourNodeData SaveToNodeData()
    {
        var nodeData = new SequenceNodeData
        {
            NodeGuid = this.GUID,
            BehaviourStuff = this.Text,
            NodePosition = this.GetPosition().position,
            NodeType = NodeType.SEQUENCE,
            RepeatPolicy = (RepeatPolicies)this.RepeatPolicyField.value,
            ReturnPolicy = (ReturnPolicies)this.ReturnPolicyField.value
        };
        return nodeData;
    }

}
