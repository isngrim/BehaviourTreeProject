using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNodeBuilder : INodeBuilder
{
    public NodeType NodeType => NodeType.MoveTo;

    public string Title => "MoveTo Node";

    public BehaviourGraphNode Build()
    {
        return new MoveToGraphNode
        {
            title = Title,
            Text = Title,
            GUID = Guid.NewGuid().ToString()
        };
    }

    public BehaviourGraphNode Build(BehaviourNodeData nodeData)
    {
        var moveToData = nodeData as MoveToNodeData;
        return new MoveToGraphNode(nodeData)
        {
            title = Title,
            Text = Title,
            GUID = Guid.NewGuid().ToString(),

        };
    }
}
