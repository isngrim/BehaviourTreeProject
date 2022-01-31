using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[Serializable]
public  class BehaviourNodeData 
{
    public string NodeGuid;
    
    //probably need to replace this with Node data
    public string BehaviourStuff;
    public Vector2 NodePosition;
    public NodeType NodeType;
    public RepeatPolicies RepeatPolicy;
    public ReturnPolicies ReturnPolicy;
    public virtual BehaviourNode BuildNode()
    { return null; }
}
