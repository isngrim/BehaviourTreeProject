using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//currrently a monobehaviour becuase it solved an issue i had with unity
[System.Serializable]
public abstract class BehaviourNode 
{
    public string Log;
    public string NodeId;
    // does the node run multiple times once completed? currently unimplemented
    public RepeatPolicies RepeatPolicy;
    // does the node always succeed or fail? 
    public ReturnPolicies ReturnPolicy;
    public NodeStates CurrentState;
    //not currently used,is meant for fallback behaviours
    public BehaviourNode ParentNode;
    //event for tracking child node states
    public delegate void BehaviourObserver(NodeStates nodeState, BehaviourNode completedChild);
    public abstract event BehaviourObserver ChildCompleteEvent;
    //all child nodes
    [SerializeReference][SerializeField]
    public List<BehaviourNode> Children = new List<BehaviourNode>();
    //Called when we start runing the behaviour
    abstract public void OnInitialize();
    //Here we do whatever the behaviour does,returns a list of BehaviourNodes that is then stored by the scheduler
    abstract public List<BehaviourNode> UpdateNode(IBlackboard blackboard);
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
