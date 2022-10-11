using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNodeData : BehaviourNodeData
{
    public string TargetPositionKey;
    public string NPCObjectKey;
    public override BehaviourNode BuildNode()
    {
        {
            BehaviourNode newNode = null;
            Debug.Log("MoveToNode");
            newNode = new MoveToNode
            {
                Log = this.NodeType.ToString(),
                NodeId = this.NodeGuid,
                ReturnPolicy = this.ReturnPolicy,
                RepeatPolicy = this.RepeatPolicy,
                PositionKey = this.TargetPositionKey,
                NPCKey = this.NPCObjectKey
            };
            return newNode;
        }
    }
}
