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
    public override bool IsTerminated()
    {

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
            this.ChildIndex = -1;
            return true;
        }
        else { return false; }
    }

    public override void OnChildComplete(NodeStates nodeState, BehaviourNode completedChild)
    {
        if (completedChild.CurrentState == NodeStates.SUCCEEDED)
        {
            this.CurrentState = NodeStates.SUCCEEDED  ;
            completedChild.ChildCompleteEvent -= OnChildComplete;
        }
        else if (completedChild == this.Children[this.Children.Count - 1])
        {
            this.CurrentState = NodeStates.FAILED;
            completedChild.ChildCompleteEvent -= OnChildComplete;
        }
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
}
