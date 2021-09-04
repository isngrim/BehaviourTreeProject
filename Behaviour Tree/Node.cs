using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourNode
{
    public bool RepeatUntilSuceed = false;
    public bool RepeatUntilFail = false;
    public bool AlwaysSuceed = false;
    public bool AlwaysFail = false;
    public NodeStates CurrentState;
    public BehaviourNode ParentNode;
    public delegate void BehaviourObserver(NodeStates nodeState);
    public abstract event BehaviourObserver ChildCompleteEvent;

    abstract public void OnInitialize();
    abstract public List<BehaviourNode> Update();
    abstract public void OnChildComplete(NodeStates nodeState);
    abstract public bool IsTerminated();

}
public enum NodeStates
{
    READY,
    RUNNING,
    FAILED,
    SUCCEEDED,
    SUSPENDED   

}
