using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
public class MoveToGraphNode : BehaviourGraphNode
{
    public string TargetPositionKey;
    public TextField TargetPositionField;
    public string NPCObjectKey;
    public TextField NPCObjectField;
    public MoveToGraphNode()
    {
        this.nodeType = NodeType.MoveTo;
        TargetPositionField = new TextField();
        TargetPositionField.label = "Target Position Key";
        TargetPositionField.RegisterCallback((EventCallback < ChangeEvent<string>>)(evt => this.TargetPositionKey = evt.newValue));
        this.extensionContainer.Add(TargetPositionField);
        NPCObjectField = new TextField();
        NPCObjectField.label = "Npc Object Key";
        NPCObjectField.RegisterCallback((EventCallback<ChangeEvent<string>>)(evt => this.NPCObjectKey = evt.newValue));
        this.extensionContainer.Add(NPCObjectField);
    }
    public MoveToGraphNode(BehaviourNodeData nodeData)
    {
        var movetoNodeData = nodeData as MoveToNodeData;
        this.nodeType = NodeType.MoveTo;
        TargetPositionField = new TextField();
        TargetPositionKey = movetoNodeData.TargetPositionKey;
        TargetPositionField.value = movetoNodeData.TargetPositionKey;
        TargetPositionField.label = "Target Position Key";
        TargetPositionField.RegisterCallback((EventCallback<ChangeEvent<string>>)(evt => this.TargetPositionKey = evt.newValue));
        this.extensionContainer.Add(TargetPositionField);
        NPCObjectField = new TextField();
        NPCObjectKey = movetoNodeData.NPCObjectKey;
        NPCObjectField.value = movetoNodeData.NPCObjectKey;
        NPCObjectField.label = "Npc Object Key";
        NPCObjectField.RegisterCallback((EventCallback<ChangeEvent<string>>)(evt => this.NPCObjectKey = evt.newValue));
        this.extensionContainer.Add(NPCObjectField);
        this.RepeatPolicy = nodeData.RepeatPolicy;
        this.ReturnPolicy = nodeData.ReturnPolicy;

    }
    public override BehaviourNodeData SaveToNodeData()
    {

        var nodeData = new MoveToNodeData
        {
            NodeGuid = this.GUID,
            BehaviourStuff = this.Text,
            NodePosition = this.GetPosition().position,
            NodeType = this.nodeType,
            RepeatPolicy = (RepeatPolicies)this.RepeatPolicyField.value,
            ReturnPolicy = (ReturnPolicies)this.ReturnPolicyField.value,
            TargetPositionKey = this.TargetPositionKey,
            NPCObjectKey = this.NPCObjectKey
        };
        return nodeData;
    }
}
