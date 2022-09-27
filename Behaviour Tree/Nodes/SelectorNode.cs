using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BehaviourNode
{
    int ChildIndex = -1;
    BehaviourNode CurrentNode;

    public override event BehaviourObserver ChildCompleteEvent;
    private void Start()
    {

    }


    public override void OnInitialize()
    {
        if (ChildIndex == -1)
        {
            this.CurrentState = NodeStates.RUNNING;
            this.ChildIndex++;
            Mathf.Clamp(this.ChildIndex, 0, this.Children.Count - 1);
            if (this.Children[0] != null) this.CurrentNode = this.Children[0];
        }
        if (this.CurrentNode != null)
        {
            this.CurrentNode.ChildCompleteEvent += OnChildComplete;
        }
        else this.CurrentState = NodeStates.FAILED;
    }

    public override List<BehaviourNode> UpdateNode()
    {
        Debug.Log(this.gameObject.name + " updating");
        Debug.Log(this.Log + "index = " + this.ChildIndex);

        List<BehaviourNode> children = new List<BehaviourNode>();
        if (RepeatPolicy != RepeatPolicies.NOREPEAT)
        {
            //  children.Add(this);
        }
        if (this.ChildIndex != -1 && this.ChildIndex <= this.Children.Count - 1 && this.CurrentState != NodeStates.FAILED)
        {
            Debug.Log( "updating");
            if (this.Children[this.ChildIndex] != null)
            {
                children.Add(this.Children[this.ChildIndex]);
            }
        }
        return children;
    }

    //note to self: i think i can move the "completedChild.ChildCompleteEvent -= OnChildComplete;" to a single line at the end
    public override void OnChildComplete(NodeStates nodeState, BehaviourNode completedChild)
    {
        //if the current child node is a success we succeed finish this node
        if (completedChild.CurrentState == NodeStates.SUCCEEDED)
        {
            this.CurrentState = NodeStates.SUCCEEDED;
            completedChild.ChildCompleteEvent -= OnChildComplete;
        }
        //if the current child node didnt succeed and we're at the end of the list,we return a failure
        else if (completedChild == this.Children[this.Children.Count - 1])
        {
            this.CurrentState = NodeStates.FAILED;
            completedChild.ChildCompleteEvent -= OnChildComplete;
        }
        //if we arent at the end of the l;ist,we move to the next node in the list
        else
        {
            this.ChildIndex++;
            if (this.ChildIndex > this.Children.Count - 1)
                this.ChildIndex = this.Children.Count - 1;

            completedChild.ChildCompleteEvent -= OnChildComplete;
            if (this.Children[this.ChildIndex] != null)
            {
                this.CurrentNode = this.Children[this.ChildIndex];
                this.CurrentNode.ChildCompleteEvent += OnChildComplete;
            }
        }
    }


    public override bool IsTerminated()
    {

        if (this.CurrentState == NodeStates.FAILED || this.CurrentState == NodeStates.SUCCEEDED)
        {
            //change the node state to match the return policy 
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
            //reset node values
            this.ChildIndex = -1;
            return true;
        }
        else return false; 
    }


}
