using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
public class LeafGraphNode : BehaviourGraphNode
{
    public Policy SuccessPolicy = Policy.REQUIRE_ALL;
public EnumField SuccessPolicyField;
public LeafGraphNode(BehaviourGraphView graph)
{
    this.nodeType = NodeType.LEAF;
}
public LeafGraphNode(BehaviourGraphView graph, BehaviourNodeData nodeData)
{
    this.nodeType = nodeData.NodeType;
        this.RepeatPolicy = nodeData.RepeatPolicy;
        this.ReturnPolicy = nodeData.ReturnPolicy;

    }


}