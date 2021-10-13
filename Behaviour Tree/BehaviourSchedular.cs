using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSchedular : MonoBehaviour
{
    Queue<BehaviourNode> ActiveBehaviours;
   public BehaviourNode Root;
    //   Blackboard Blackboard;
    private void Start()
    {
        this.ActiveBehaviours = new Queue<BehaviourNode>();
        if(Root != null)
        this.ActiveBehaviours.Enqueue(Root);

    }
    bool Step()
    {
        BehaviourNode currentNode = this.ActiveBehaviours.Dequeue();
        if (currentNode == null) return false;
        currentNode.OnInitialize();
        List<BehaviourNode> returnedNodes = currentNode.UpdateNode();
        foreach(var node in returnedNodes)
        {
            if(node != null)
            this.ActiveBehaviours.Enqueue(node);
        }
        if (currentNode.IsTerminated())
        {
            //log completion or do anything else related to the completion of a node
        }
        else
        {
            this.ActiveBehaviours.Enqueue(currentNode);
        }
            return true;
    }
    protected void Tick()
    {
        this.ActiveBehaviours.Enqueue(null);
        while (Step()) { }//?
    }
    private void Update()
    {
        if (this.ActiveBehaviours.Count > 0)
        {
            Tick();
        }
        else
        {
          this.ActiveBehaviours.Enqueue(this.Root);
        }
    }
}
