using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSchedular : MonoBehaviour
{
    Queue<BehaviourNode> ActiveBehaviours;
    BehaviourNode Root;
 //   Blackboard Blackboard;
    bool Step()
    {
        BehaviourNode currentNode = this.ActiveBehaviours.Dequeue();
        if (currentNode == null) return false;
        currentNode.OnInitialize();
        List<BehaviourNode> returnedNodes = currentNode.Update();
        foreach(var node in returnedNodes)
        {
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

    }
}
