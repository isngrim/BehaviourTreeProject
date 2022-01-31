using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class SelectorGraphNode : BehaviourGraphNode
{

    public SelectorGraphNode(BehaviourGraphView graph)
    {
        this.nodeType = NodeType.SELECTOR;
    }
    public SelectorGraphNode(BehaviourGraphView graph,SelectorNodeData nodeData)
    {
        this.nodeType = NodeType.SELECTOR;
        this.RepeatPolicy = nodeData.RepeatPolicy;
        this.ReturnPolicy = nodeData.ReturnPolicy;
    }
    public override BehaviourNodeData SaveToNodeData()
    {

        var nodeData = new SelectorNodeData()
        {
            NodeGuid = this.GUID,
            BehaviourStuff = this.Text,
            NodePosition = this.GetPosition().position,
            NodeType = this.nodeType,
            RepeatPolicy = (RepeatPolicies)RepeatPolicyField.value,
            ReturnPolicy = (ReturnPolicies)ReturnPolicyField.value
        };
        return nodeData;
    }
}