using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : BehaviourNode
{
    int ChildIndex = -1;
    [SerializeReference]
    BehaviourNode CurrentNode;

    public override event BehaviourObserver ChildCompleteEvent;


    public override void OnInitialize()
    {
        if (ChildIndex == -1)
        {
            // if(IsRandomSequence)
            // RandomizeChildren();
            this.CurrentState = NodeStates.RUNNING;
            this.ChildIndex++;
            Mathf.Clamp(this.ChildIndex, 0, this.Children.Count - 1);
            if (this.Children.Count >0) this.CurrentNode = this.Children[0];
        }
        if (this.CurrentNode != null)
        {
            this.CurrentNode.ChildCompleteEvent += OnChildComplete;
        }
        else this.CurrentState = NodeStates.FAILED;
    }


    public override List<BehaviourNode> UpdateNode()
    {

        List<BehaviourNode> children = new List<BehaviourNode>();
        /* //not sure if this is how repeating nodes will be handled
        if(RepeatPolicy != RepeatPolicies.NOREPEAT)
        {
            children.Add(this);
        }*/
        //add next child in sequence to the schedular
       if(this.ChildIndex != -1 &&  this.ChildIndex  <=this.Children.Count-1 && this.CurrentState != NodeStates.FAILED)
       {
            if (this.Children[this.ChildIndex] != null)
            {
                children.Add(this.Children[this.ChildIndex]);
            }
       }
        return children;
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

    //note to self: i think i can move the "completedChild.ChildCompleteEvent -= OnChildComplete;" to a single line at the end
    public override void OnChildComplete(NodeStates nodeState, BehaviourNode completedChild)
    {
        //if any child in the sequence fails,we fail.
        if (completedChild.CurrentState == NodeStates.FAILED)
        {
            this.CurrentState = NodeStates.FAILED;
            completedChild.ChildCompleteEvent -= OnChildComplete;
        }
        //if were a the end of the list,the sequence has succeeded 
        else if (completedChild == this.Children[this.Children.Count - 1])
        {
            this.CurrentState = NodeStates.SUCCEEDED;
            completedChild.ChildCompleteEvent -= OnChildComplete;
        }
        //otherwise,we move to next node in sequence
        else
        {
            this.ChildIndex++;
            Mathf.Clamp(this.ChildIndex, 0, this.Children.Count - 1);

            completedChild.ChildCompleteEvent -= OnChildComplete;
            if (this.Children[this.ChildIndex] != null)
            {
                this.CurrentNode = this.Children[this.ChildIndex];
                this.CurrentNode.ChildCompleteEvent += OnChildComplete;
            }
        }
    }


}
