using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class MoveToNode : BaseLeafNode
{
    public string NPCKey;
    public string PositionKey;
    public override NodeStates HandleNodeBehaviour(IBlackboard blackboard)
    {
        GameObject character = (GameObject)blackboard.KnowledgeBase[NPCKey];
        Vector3 targetPos = (Vector3)blackboard.KnowledgeBase[PositionKey];
        if (Vector3.Distance(character.transform.position,targetPos )> 0.01f)
        {
            var navAgent = character.GetComponent<NavMeshAgent>();
            navAgent.SetDestination(targetPos);
            return NodeStates.SUCCEEDED;
        }

        return NodeStates.RUNNING ;

    }


}
