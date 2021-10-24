using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

public class BehaviourGraphView : GraphView
{
    private readonly Vector2 DefaultNodeSize = new Vector2(x: 150,y: 200);
    public BehaviourGraphView()
    {
        
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        AddElement(GenerateEntryPointNode());
    }
    private Port GeneratePort(BehaviourGraphNode node,Direction portDirection,Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Vertical, portDirection, capacity, type: typeof(float));//will need to look into the port
    }
    private BehaviourGraphNode GenerateEntryPointNode()
    {
        var node = new BehaviourGraphNode
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            Text = "EntryPoint",
            EntryPoint = true

        };

        var generatedPort =  GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);
        node.RefreshExpandedState();
        node.RefreshPorts();
        node.SetPosition(new Rect(x: 100, y: 200, width: 100, height: 150));
        return node;
    }
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        ports.ForEach(funcCall: (port) =>
        {
            if (startPort != port && startPort.node != port.node) compatiblePorts.Add(port);
        });
        return compatiblePorts;
    }
    public void CreateNode(string nodeName)
    {
        this.AddElement(CreateBehaviourNode(nodeName));
    }
    public BehaviourGraphNode CreateBehaviourNode(string nodeName)
    {
        var behaviourNode = new BehaviourGraphNode
        {
            title = nodeName,
            Text = nodeName,
            GUID = Guid.NewGuid().ToString()
        };

        var inputPort = GeneratePort(behaviourNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        behaviourNode.inputContainer.Add(inputPort);

        var button = new Button(clickEvent: () => AddChoicePort(behaviourNode));
        behaviourNode.RefreshExpandedState();
        behaviourNode.RefreshPorts();
        behaviourNode.SetPosition(new Rect(position: Vector2.zero, this.DefaultNodeSize));
        return behaviourNode;
    }

    private void AddChoicePort(BehaviourGraphNode behaviourNode)
    {
        var generatedPort = GeneratePort(behaviourNode, Direction.Output);
        var outputPortCount = behaviourNode.outputContainer.Query(name: "connector").ToList().Count;
        var outputPortName = $"Choice{outputPortCount}";
        behaviourNode.outputContainer.Add(generatedPort);
    }
}
