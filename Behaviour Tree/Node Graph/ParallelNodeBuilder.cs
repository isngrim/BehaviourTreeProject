using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelNodeBuilder : INodeBuilder
{
    public NodeType NodeType => NodeType.PARALLEL;

    public string Title => "Parallel Node";
    public BehaviourGraphNode Build()
    {
        return new ParallelGraphNode
        {
            title = Title,
            Text = Title,
            GUID = Guid.NewGuid().ToString()
        };
    }

    public BehaviourGraphNode Build(BehaviourNodeData nodeData)
    {
        var parallelNodeData = (ParallelNodeData)nodeData;
        return new ParallelGraphNode(nodeData)
        {
            title = Title,
            Text = Title,
            GUID = Guid.NewGuid().ToString(),
        };
    }
}
