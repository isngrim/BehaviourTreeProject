using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLeafNode : BehaviourNode
{
    public override event BehaviourObserver ChildCompleteEvent;


    public override void OnInitialize()
    {

        if (this.CurrentState == NodeStates.READY)
        {
            this.CurrentState = NodeStates.RUNNING;
        }

    }

    public override List<BehaviourNode> UpdateNode(IBlackboard  blackboard )
    {
        this.CurrentState = HandleNodeBehaviour(blackboard);
        var childList = new List<BehaviourNode>();
        childList.Add(this);
        return childList;
    }
    public abstract NodeStates HandleNodeBehaviour(IBlackboard blackboard);

    public override bool IsTerminated()
    {
        if (this.CurrentState == NodeStates.FAILED || this.CurrentState == NodeStates.SUCCEEDED)
        {
            //change the return state to match the return policy 
            if (ReturnPolicy == ReturnPolicies.ALWAYSUCCEED)
            {
                this.CurrentState = NodeStates.SUCCEEDED;
            }
            else if (ReturnPolicy == ReturnPolicies.ALWAYSFAIL)
            {
                this.CurrentState = NodeStates.FAILED;
            }
            //fire event to let any listeners know this node is done
            if (this.ChildCompleteEvent != null) this.ChildCompleteEvent(this.CurrentState, this);
            this.CurrentState = NodeStates.READY;

            return true;
        }
        else return false;
    }
    public override void OnChildComplete(NodeStates nodeState, BehaviourNode completedChild)
    {
        //there are no children
    }


}
