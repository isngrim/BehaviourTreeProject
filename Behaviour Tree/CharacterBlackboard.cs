using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlackboard : MonoBehaviour, IBlackboard
{
    public Dictionary<string, object> KnowledgeBase { get; set; }

    public event KnowledgeObserver KnowledgeUpdateEvent;

    // Start is called before the first frame update

    void Start()
    {
        KnowledgeBase = new Dictionary<string, object>();
        //temporarily hardcoded,should normally be coming from some knowledge source
        KnowledgeBase["Character_Gameobject"] = this.gameObject;
        KnowledgeBase["TargetPosition_Vector3"] = Vector3.zero;
    }

    public object GetKnowledge(string KnowledgeKey)
    {
        return KnowledgeBase[KnowledgeKey];
    }

    public void SetKnowledge(string KnowledgeKey, object KnowledgeValue)
    {
        KnowledgeBase[KnowledgeKey] = KnowledgeValue;
        this.KnowledgeUpdateEvent(KnowledgeKey);
    }
}
