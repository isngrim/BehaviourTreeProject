using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourNode : MonoBehaviour
{
    public string Log;
    public string NodeId;
    // does the node run multiple times once completed? currently unimplemented
    public RepeatPolicies RepeatPolicy;
    // does the node always succeed or fail? 
    public ReturnPolicies ReturnPolicy;
    public NodeStates CurrentState;
    public BehaviourNode ParentNode;
    public delegate void BehaviourObserver(NodeStates nodeState, BehaviourNode completedChild);
    public abstract event BehaviourObserver ChildCompleteEvent;
    public List<BehaviourNode> Children;
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
public enum RepeatPolicies
{
    NOREPEAT,
    REPEATUNTILFAIL,
    REPEATUNTILSUCCEED
}
public enum ReturnPolicies
{
    DEFAULT,
    ALWAYSUCCEED,
    ALWAYSFAIL,
}
public enum NodeType
{
    SELECTOR,
    SEQUENCE,
    PARALLEL,
}