using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceGraphNode : BehaviourGraphNode
{
    public SequenceGraphNode(BehaviourGraphView graph)
    {
        this.nodeType = NodeType.SEQUENCE;
    }
    public SequenceGraphNode(BehaviourGraphView graph,SequenceNodeData nodeData)
    {
        this.nodeType = nodeData.NodeType;
    }
    public override BehaviourNodeData SaveToNodeData()
    {
        var nodeData = new SequenceNodeData()
        {
            NodeGuid = this.GUID,
            BehaviourStuff = this.Text,
            NodePosition = this.GetPosition().position,
            NodeType = NodeType.SEQUENCE,
            RepeatPolicy = (RepeatPolicies)RepeatPolicyField.value,
            ReturnPolicy = (ReturnPolicies)ReturnPolicyField.value
        };
        return nodeData;
    }

}
