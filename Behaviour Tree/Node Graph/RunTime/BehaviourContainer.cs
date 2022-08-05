using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BehaviourContainer : ScriptableObject
{
    [SerializeField]
    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    [SerializeField]
    public List<BehaviourNodeData> BehaviourNodeData = new List<BehaviourNodeData>();
}
