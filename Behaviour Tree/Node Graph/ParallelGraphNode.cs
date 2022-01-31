using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class ParallelGraphNode : BehaviourGraphNode
{
    public Policy SuccessPolicy =Policy.REQUIRE_ALL;
    public EnumField SuccessPolicyField;
    public ParallelGraphNode(BehaviourGraphView graph)
    {
        this.nodeType = NodeType.PARALLEL;

        SuccessPolicyField = new EnumField(Policy.REQUIRE_ALL);
        SuccessPolicyField.label = "Success Policy";
        SuccessPolicyField.RegisterCallback((EventCallback<ChangeEvent<Policy>>)(evt => this.SuccessPolicy = evt.newValue));
        this.extensionContainer.Add(SuccessPolicyField);
        
    }
    public ParallelGraphNode(BehaviourGraphView graph,ParallelNodeData nodeData)
    {
        this.nodeType = nodeData.NodeType;

        SuccessPolicyField = new EnumField(nodeData.SuccessPolicy);
        SuccessPolicyField.label = "Success Policy";
        SuccessPolicyField.value = nodeData.SuccessPolicy;
        SuccessPolicyField.RegisterCallback((EventCallback<ChangeEvent<Policy>>)(evt => this.SuccessPolicy = evt.newValue));
        this.extensionContainer.Add(SuccessPolicyField);
        
    }

    public override BehaviourNodeData SaveToNodeData()
    {
        var nodeData = new ParallelNodeData()
        {

            NodeGuid = this.GUID,
            BehaviourStuff = this.Text,
            NodePosition = this.GetPosition().position,
            SuccessPolicy = (Policy)SuccessPolicyField.value,
            NodeType = NodeType.PARALLEL,
            RepeatPolicy = (RepeatPolicies)RepeatPolicyField.value,
            ReturnPolicy = (ReturnPolicies)ReturnPolicyField.value
        };
        return nodeData;
    }
}
