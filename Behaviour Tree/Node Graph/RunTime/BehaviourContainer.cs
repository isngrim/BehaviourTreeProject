using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BehaviourContainer : ScriptableObject
{
    
    public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
    public List<BehaviourNodeData> BehaviourNodeData = new List<BehaviourNodeData>();
}
