using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SequenceNodeData : BehaviourNodeData
{
    public override BehaviourNode BuildNode()
    {
        Debug.Log("SEQUENCENode");
      var  newNode = new SequenceNode()
        {
            Log = this.NodeType.ToString(),
            NodeId = this.NodeGuid,
            ReturnPolicy = this.ReturnPolicy,
            RepeatPolicy = this.RepeatPolicy
        };
        return newNode;
    }
}
