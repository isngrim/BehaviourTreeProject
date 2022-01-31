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

    public override bool IsTerminated()
    {
       // Debug.Log(this.gameObject.name + " " + this.CurrentState);


        if (this.CurrentState == NodeStates.FAILED || this.CurrentState == NodeStates.SUCCEEDED)
        {

            if (ReturnPolicy == ReturnPolicies.ALWAYSUCCEED)
            {
                this.CurrentState = NodeStates.SUCCEEDED;
            }
            else if (ReturnPolicy == ReturnPolicies.ALWAYSFAIL)
            {
                this.CurrentState = NodeStates.FAILED;
            }
            if (this.ChildCompleteEvent != null) this.ChildCompleteEvent(this.CurrentState, this);
            foreach (var child in this.Children)
            {
                child.ChildCompleteEvent -= OnChildComplete;

            }
            this.FailureCount = 0;
            this.SuccessCount = 0;
            this.CurrentState = NodeStates.READY;

  
            return true;
        }
        else { return false; }
    }

    public override void OnChildComplete(NodeStates nodeState, BehaviourNode completedChild)
    {

        if (nodeState == NodeStates.FAILED)
        {
            this.FailureCount++;
           

           // completedChild.ChildCompleteEvent -= OnChildComplete;
        }
        else if(nodeState == NodeStates.SUCCEEDED)
        {
            this.SuccessCount++;
            //  completedChild.ChildCompleteEvent -= OnChildComplete;

        }
        if (this.SuccessCount > 0 )
        {
            if (this.SuccessPolicy == Policy.REQUIRE_ONE)
            {
                this.CurrentState = NodeStates.SUCCEEDED;
            }
        }


        if (this.SuccessPolicy == Policy.REQUIRE_ALL && this.SuccessCount >= this.Children.Count - 1)
        {
            this.CurrentState = NodeStates.SUCCEEDED;
        }
        if (this.SuccessPolicy == Policy.REQUIRE_ALL && this.FailureCount > 0)
        {
            this.CurrentState = NodeStates.FAILED;
        } 
    }

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
        if (RepeatPolicy != RepeatPolicies.NOREPEAT)
        {
            // children.Add(this);
        }
        if (this.Children.Count > 0 && this.CurrentState != NodeStates.FAILED)
        {
            // Debug.Log(this.gameObject.name + "updating");
          foreach(var child in this.Children)
            {
                Debug.Log(this.Log + " running  " + child.Log);
                children.Add(child);
            }
        }
        return children;
    }
}
public enum Policy
{
    REQUIRE_ONE,
    REQUIRE_ALL

}
