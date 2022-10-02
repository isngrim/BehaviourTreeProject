using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNodeBuilder : INodeBuilder
{
    public NodeType NodeType => NodeType.SELECTOR;

    public string Title => "Selector Node";

    public BehaviourGraphNode Build()
    {
        return  new SelectorGraphNode()
        {
            title = Title,
            Text = Title,
            GUID = Guid.NewGuid().ToString()
        };
    }
}
