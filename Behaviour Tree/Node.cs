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

    //Called when we start runing the behaviour
    abstract public void OnInitialize();
    //Here we do whatever the behaviour does,returns a list of BehaviourNodes that is then stored by the scheduler
    abstract public List<BehaviourNode> Update();
    //Called on recieving the ChildCompleteEvent from the child
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
