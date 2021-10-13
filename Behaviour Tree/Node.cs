using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourNode : MonoBehaviour
{
    public string Log;
    public bool Repeating = false;
    public bool RepeatUntilSuceed = false;
    public bool RepeatUntilFail = false;
    public bool AlwaysSuceed = false;
    public bool AlwaysFail = false;
    public NodeStates CurrentState;
    public BehaviourNode ParentNode;
    public delegate void BehaviourObserver(NodeStates nodeState, BehaviourNode completedChild);
    public abstract event BehaviourObserver ChildCompleteEvent;

    //Called when we start runing the behaviour
    abstract public void OnInitialize();
    //Here we do whatever the behaviour does,returns a list of BehaviourNodes that is then stored by the scheduler
    abstract public List<BehaviourNode> UpdateNode();
    //Called on recieving the ChildCompleteEvent from the child
    abstract public void OnChildComplete(NodeStates nodeState,BehaviourNode completedChild);
    
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
