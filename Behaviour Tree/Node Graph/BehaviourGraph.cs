using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class BehaviourGraph : EditorWindow
{
    private BehaviourGraphView GraphView;

   [ MenuItem("Graph/Behaviour Tree Graph")]
    public static void OpenBehaviourGraphWindow()
    {
        var window = GetWindow<BehaviourGraph>();
        window.titleContent = new GUIContent(text: "Behaviour Tree Editor");
    }
    private void ConstructGraphView()
    {
        this.GraphView = new BehaviourGraphView
        {
            name = "Behaviuor Graph"

        };
        this.GraphView.StretchToParentSize();
        rootVisualElement.Add(this.GraphView);
    }
    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var nodeCreateButton = new Button(clickEvent:() =>
        {
            this.GraphView.CreateNode("Behaviour Node");
        });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }
    private void OnEnable()
    {
        this.ConstructGraphView();
        this.GenerateToolbar();
    }
    private void OnDisable()
    {
        rootVisualElement.Remove(this.GraphView);
    }
}
