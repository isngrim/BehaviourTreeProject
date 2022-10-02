 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
[System.Serializable]
public abstract class BehaviourGraphNode : Node
{
    public string GUID;
    public BehaviourNode BehaviourNode;
    public bool EntryPoint;
    public string Text;
    public NodeType nodeType;
    public RepeatPolicies RepeatPolicy = RepeatPolicies.NOREPEAT;
    public ReturnPolicies ReturnPolicy = ReturnPolicies.DEFAULT;
    public NodeStates CurrentState;
    public EnumField RepeatPolicyField;
    public EnumField ReturnPolicyField;
    public virtual BehaviourNodeData SaveToNodeData() { throw new System.NotImplementedException(); }

}
