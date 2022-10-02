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
}
