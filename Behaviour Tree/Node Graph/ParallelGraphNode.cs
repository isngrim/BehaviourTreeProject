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
    public ParallelGraphNode(BehaviourGraphView graph,BehaviourNodeData nodeData)
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
        BehaviourNodeData nodeData = new BehaviourNodeData()
        {

            NodeGuid = this.GUID,
            BehaviourStuff = this.Text,
            NodePosition = this.GetPosition().position,
            SuccessPolicy = (Policy)this.SuccessPolicyField.value,
            NodeType = NodeType.PARALLEL,
            RepeatPolicy = (RepeatPolicies)this.RepeatPolicyField.value,
            ReturnPolicy = (ReturnPolicies)this.ReturnPolicyField.value
        };
        return nodeData;
    }
}
