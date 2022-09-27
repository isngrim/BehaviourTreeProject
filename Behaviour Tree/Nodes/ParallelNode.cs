using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelNode : BehaviourNode
{
    public Policy SuccessPolicy;

    //int ChildIndex = -1;
    public int FailureCount = 0;
    public int SuccessCount = 0;
    
    BehaviourNode CurrentNode;

    public override event BehaviourObserver ChildCompleteEvent;


    public override void OnInitialize()
    {
       
        if (this.CurrentState == NodeStates.READY)
        {
            this.CurrentState = NodeStates.RUNNING;
            foreach (var child in this.Children)
            {
                if (child != null)
                {
                    child.ChildCompleteEvent += OnChildComplete;
                }
                else this.CurrentState = NodeStates.FAILED;
            }
        }
        
    }

    public override List<BehaviourNode> UpdateNode()
    {
        List<BehaviourNode> children = new List<BehaviourNode>();
        /* //not sure if this is how repeating nodes will be handled
        if (RepeatPolicy != RepeatPolicies.NOREPEAT)
        {
             children.Add(this);
        }
        */
        //add children to schedular
        if (this.Children.Count > 0 && this.CurrentState != NodeStates.FAILED)
        {        
          foreach(var child in this.Children)
            {
                children.Add(child);
            }
        }
        return children;
    }

    public override void OnChildComplete(NodeStates nodeState, BehaviourNode completedChild)
    {
        //track how many successes and failures
        if (nodeState == NodeStates.FAILED)
        { 
            this.FailureCount++;
        }
        else if (nodeState == NodeStates.SUCCEEDED)
        {
            this.SuccessCount++;
        }
        //check if the node only requires one child to succeed
        if (this.SuccessPolicy == Policy.REQUIRE_ONE)
        {
            if (this.SuccessCount > 0)
            {
                this.CurrentState = NodeStates.SUCCEEDED;
            }
        }
        // check if the node requires all to succeed
        if (this.SuccessPolicy == Policy.REQUIRE_ALL && this.SuccessCount >= this.Children.Count - 1)
        {
            this.CurrentState = NodeStates.SUCCEEDED;
        } //note to self* this can probably be cleaned up
        if (this.SuccessPolicy == Policy.REQUIRE_ALL && this.FailureCount > 0)
        {
            this.CurrentState = NodeStates.FAILED;
        }
    }

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
            // stop listening to child nodes
            foreach (var child in this.Children)
            {
                child.ChildCompleteEvent -= OnChildComplete;
            }
            //reset node values
            this.FailureCount = 0;
            this.SuccessCount = 0;
            this.CurrentState = NodeStates.READY;

            return true;
        }
        else return false;
    }


}
public enum Policy
{
    REQUIRE_ONE,
    REQUIRE_ALL

}
