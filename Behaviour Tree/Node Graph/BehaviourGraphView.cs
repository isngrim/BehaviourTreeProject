using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using System.Linq;
using UnityEditor.UIElements;

public class BehaviourGraphView : GraphView
{
    public readonly Vector2 DefaultNodeSize = new Vector2(x: 150,y: 200);
    private readonly NodeDataSource nodeDataSource;

    public BehaviourGraphView(NodeDataSource nodeDataSource)
    {

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        var grid = new GridBackground();
        Insert(index: 0, grid);
        grid.StretchToParentSize();
        AddElement(GenerateEntryPointNode());
        this.nodeDataSource = nodeDataSource;

    }
    private Port GeneratePort(BehaviourGraphNode node,Direction portDirection,Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Vertical, portDirection, capacity, type: typeof(float));//will need to look into the port
    }
    private BehaviourGraphNode GenerateEntryPointNode()
    {
        var node = new SelectorGraphNode()
        {
            title = "Root",
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
    public void CreateNode(string nodeName,NodeType nodeType)
    {
        this.AddElement(CreateBehaviourNode(nodeName,nodeType));
    }
    public BehaviourGraphNode CreateBehaviourNode(string nodeName, NodeType nodeType)
    {
        BehaviourGraphNode behaviourNode = nodeDataSource.BuildGraphNode(nodeType);
        var inputPort = GeneratePort(behaviourNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        behaviourNode.inputContainer.Add(inputPort);

        behaviourNode.RepeatPolicyField = new EnumField(behaviourNode.RepeatPolicy);
        behaviourNode.RepeatPolicyField.value = RepeatPolicies.NOREPEAT;
        behaviourNode.RepeatPolicyField.RegisterCallback((EventCallback<ChangeEvent<RepeatPolicies>>)(evt => behaviourNode.RepeatPolicy = evt.newValue));
        behaviourNode.RepeatPolicyField.label = "Is Repeating";
        behaviourNode.extensionContainer.Add(behaviourNode.RepeatPolicyField);
        behaviourNode.ReturnPolicyField = new EnumField(behaviourNode.ReturnPolicy);

        behaviourNode.ReturnPolicyField.RegisterCallback((EventCallback<ChangeEvent<ReturnPolicies>>)(evt => behaviourNode.ReturnPolicy = evt.newValue));
        behaviourNode.ReturnPolicyField.label = "Return Policy";
        behaviourNode.extensionContainer.Add(behaviourNode.ReturnPolicyField);
        var button = new Button(clickEvent: () => AddChoicePort(behaviourNode));
        button.text = "New Port";
        behaviourNode.titleContainer.Add(button);

        behaviourNode.RefreshExpandedState();
        behaviourNode.RefreshPorts();
        behaviourNode.SetPosition(new Rect(position: Vector2.zero, this.DefaultNodeSize));
        return behaviourNode;
    }
    public BehaviourGraphNode CreateBehaviourNode(BehaviourNodeData nodeData)
    {
        BehaviourGraphNode behaviourNode = nodeDataSource.BuildGraphNode(nodeData);
 
        var inputPort = GeneratePort(behaviourNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        behaviourNode.inputContainer.Add(inputPort);

        behaviourNode.RepeatPolicyField = new EnumField(behaviourNode.RepeatPolicy);
        behaviourNode.RepeatPolicyField.value = behaviourNode.RepeatPolicy;
        behaviourNode.RepeatPolicyField.RegisterCallback((EventCallback<ChangeEvent<RepeatPolicies>>)(evt => behaviourNode.RepeatPolicy = evt.newValue));
        behaviourNode.RepeatPolicyField.label = "Is Repeating";
        behaviourNode.extensionContainer.Add(behaviourNode.RepeatPolicyField);
        var successPolicy = new EnumField(nodeData.ReturnPolicy);
        successPolicy.RegisterCallback((EventCallback<ChangeEvent<ReturnPolicies>>)(evt => behaviourNode.ReturnPolicy = evt.newValue));
        successPolicy.label = "Return Policy";
        behaviourNode.extensionContainer.Add(successPolicy);
        var button = new Button(clickEvent: () => AddChoicePort(behaviourNode));
        button.text = "New Port";
        behaviourNode.titleContainer.Add(button);

        behaviourNode.RefreshExpandedState();
        behaviourNode.RefreshPorts();
        behaviourNode.SetPosition(new Rect(position: Vector2.zero, this.DefaultNodeSize));
        return behaviourNode;
    }
    public void AddChoicePort(BehaviourGraphNode behaviourNode,string overriddenPortName = "")
    {
        var generatedPort = GeneratePort(behaviourNode, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>(name: "type");
        generatedPort.contentContainer.Remove(oldLabel);
        var outputPortCount = behaviourNode.outputContainer.Query(name: "connector").ToList().Count;
        var choicePortName = string.IsNullOrEmpty(overriddenPortName) ? $"Choice{outputPortCount + 1}" : overriddenPortName;

        var textField = new TextField
        {
            name = string.Empty,
            value = choicePortName
        };
        textField.RegisterValueChangedCallback((EventCallback<ChangeEvent<string>>)(evt => generatedPort.portName = evt.newValue));
      
        generatedPort.contentContainer.Add(child: new Label(text: ""));
        generatedPort.contentContainer.Add(textField);
        var deleteButton = new Button(clickEvent: () => RemovePort(behaviourNode, generatedPort))
        {
            text = "x"
        };
        generatedPort.contentContainer.Add(deleteButton);
        generatedPort.portName = choicePortName;
        behaviourNode.outputContainer.Add(generatedPort);
        behaviourNode.RefreshExpandedState();
        behaviourNode.RefreshPorts();
    }

    private void RemovePort(BehaviourGraphNode behaviourNode, Port generatedPort)
    {
        var targetEdge = this.edges.ToList().Where(x => 
        x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);

        if (targetEdge.Any())
        {
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }
        behaviourNode.outputContainer.Remove(generatedPort);
        behaviourNode.RefreshPorts();
        behaviourNode.RefreshExpandedState();
    }
}
