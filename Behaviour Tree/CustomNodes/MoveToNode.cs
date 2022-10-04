using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNode : BaseLeafNode
{
    public override NodeStates HandleNodeBehaviour(IBlackboard blackboard)
    {
        return NodeStates.RUNNING ;

    }


}
