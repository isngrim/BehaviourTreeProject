using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceGraphNode : BehaviourGraphNode
{
    public SequenceGraphNode(BehaviourGraphView graph)
    {
        this.nodeType = NodeType.SEQUENCE;
    }
    public SequenceGraphNode(BehaviourGraphView graph, BehaviourNodeData nodeData)
    {
        this.nodeType = nodeData.NodeType;
    }
    public override BehaviourNodeData SaveToNodeData()
    {
        var nodeData = new BehaviourNodeData()
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
