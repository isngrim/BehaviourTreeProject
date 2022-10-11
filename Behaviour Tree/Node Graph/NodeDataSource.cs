using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeDataSource
{
    Dictionary<NodeType, INodeBuilder> NodeFactory = new Dictionary<NodeType, INodeBuilder>();
    public string[] NodeOptions => NodeFactory.Values.Select(x => x.Title).ToArray();
    public NodeType[] NodeTypeOptions => NodeFactory.Keys.ToArray();
    public BehaviourGraphNode BuildGraphNode(NodeType nodeType)
    {
        return NodeFactory[nodeType].Build();
    }
    public BehaviourGraphNode BuildGraphNode(BehaviourNodeData nodeData)
    {
        return NodeFactory[nodeData.NodeType].Build(nodeData);
    }
    public void RegisterNodeBuilder(INodeBuilder nodeBuilder)
    {
        NodeFactory[nodeBuilder.NodeType] = nodeBuilder;
    }

}
public interface INodeBuilder
{
    NodeType NodeType { get; }
    string Title { get; }
    BehaviourGraphNode Build();
    BehaviourGraphNode Build(BehaviourNodeData nodeData);
}
public static class NodeDataSourceExtensions
{
    
    public static void RegisterAllNodeBuilders(this NodeDataSource dataSource)
    {
        dataSource.RegisterNodeBuilder(new SequenceNodeBuilder());  
        dataSource.RegisterNodeBuilder(new ParallelNodeBuilder());  
        dataSource.RegisterNodeBuilder(new SelectorNodeBuilder());
        dataSource.RegisterNodeBuilder(new MoveToNodeBuilder());
    }
}
public enum NodeType
{
    SELECTOR,
    SEQUENCE,
    PARALLEL,
    MoveTo
}