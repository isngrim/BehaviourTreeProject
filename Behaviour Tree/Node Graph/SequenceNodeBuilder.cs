using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNodeBuilder : INodeBuilder
{
    public NodeType NodeType => NodeType.SEQUENCE;

    public string Title => "Sequence Node";

    public BehaviourGraphNode Build()
    {
       return new SequenceGraphNode()
       {
           title = Title,
           Text = Title,
           GUID = Guid.NewGuid().ToString()
       };
    }

    public BehaviourGraphNode Build(BehaviourNodeData nodeData)
    {
        return new SequenceGraphNode(nodeData)
        {
            title = Title,
            Text = Title,
            GUID = Guid.NewGuid().ToString()
        };
    }
}
