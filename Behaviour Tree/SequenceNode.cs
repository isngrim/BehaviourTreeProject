using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : BehaviourNode
{
    int ChildIndex = 0;
    List<BehaviourNode> Children;
    BehaviourNode CurrentNode;
    public override event BehaviourObserver ChildCompleteEvent;

    public override bool IsTerminated()
    {

        if (this.CurrentState == NodeStates.FAILED || this.CurrentState == NodeStates.SUCCEEDED)
        {

            this.ChildCompleteEvent(this.CurrentState);
            return true;
        }
        else return false;
    }

    public override void OnChildComplete(NodeStates nodeState)
    {
        /*   */
        if(this.CurrentNode.CurrentState == NodeStates.FAILED)
        {
            this.CurrentState = NodeStates.FAILED;
            this.CurrentNode.ChildCompleteEvent -= OnChildComplete;
        }
        else if(this.CurrentNode == this.Children[this.Children.Count-1])
        {
            this.CurrentState = NodeStates.SUCCEEDED;
            this.CurrentNode.ChildCompleteEvent -= OnChildComplete;
        }
        else
        {
            this.CurrentNode.ChildCompleteEvent -= OnChildComplete;
            this.CurrentNode = this.Children[++this.ChildIndex];
            if (this.Children[this.ChildIndex] != null) this.CurrentNode = this.Children[0];
            this.CurrentNode.ChildCompleteEvent += OnChildComplete;
        }
    }

    public override void OnInitialize()
    {

        this.ChildIndex = 0;
        if (this.Children[this.ChildIndex] != null) this.CurrentNode = this.Children[0];
        if (this.CurrentNode != null)
        {
            this.CurrentNode.ChildCompleteEvent += OnChildComplete;
        }
        else this.CurrentState = NodeStates.FAILED;
    }

    public override List<BehaviourNode> Update()
    {
        List<BehaviourNode> children = null;
       if(this.ChildIndex  <this.Children.Count && this.CurrentState != NodeStates.FAILED)
       {
            children.Add(this.Children[this.ChildIndex]);
       }
        return children;
    }

}
