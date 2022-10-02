using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
public abstract class BehaviourNodeData 
{
    public string NodeGuid;
    public Policy SuccessPolicy = Policy.REQUIRE_ALL;
    //probably need to replace this with Node data
    public string BehaviourStuff;
    public Vector2 NodePosition;
    public NodeType NodeType;
    public RepeatPolicies RepeatPolicy;
    public ReturnPolicies ReturnPolicy;
    public abstract BehaviourNode BuildNode();

}
