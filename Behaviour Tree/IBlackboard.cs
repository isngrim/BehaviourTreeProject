using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void KnowledgeObserver();
public interface IBlackboard 
{
    Dictionary<string, object> KnowledgeBase { get; set; }
    event KnowledgeObserver KnowledgeUpdateEvent;
    object GetKnowledge(string KnowledgeKey);
    void SetKnowledge(string KnowledgeKey, object KnowledgeValue);

}
