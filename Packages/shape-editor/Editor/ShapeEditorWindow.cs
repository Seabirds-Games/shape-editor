using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;

public class ShapeEditorWindow : EditorWindow
{
    private ShapeEditorGraphView _shapeEditorGraphView;
    private ShapeTemplate _shapeTemplate;

    [OnOpenAsset(1)]
    private static bool Callback(int instanceID, int line)
    {
        var shape = EditorUtility.InstanceIDToObject(instanceID) as ShapeTemplate;
        if (shape != null)
        {
            OpenWindow(shape);
            return true;
        }

        return false;
    }

    private static void OpenWindow(ShapeTemplate shapeTemplate)
    {
        var window = GetWindow<ShapeEditorWindow>();
        window.titleContent = new GUIContent("Shape Template Editor");
        window._shapeTemplate = shapeTemplate;
        window.rootVisualElement.Clear();
        window.CreateGraphView();
        window.CreateToolbar();
    }

    private void CreateToolbar()
    {
        var toolbar = new Toolbar();
        var clearBtn = new ToolbarButton(() => { _shapeTemplate.points.Clear(); }) {text = "Clear"};
        var undoBtn = new ToolbarButton(() => { _shapeTemplate.points.RemoveAt(_shapeTemplate.points.Count - 1); })
            {text = "Undo"};

        toolbar.Add(clearBtn);
        toolbar.Add(new ToolbarSpacer());
        toolbar.Add(undoBtn);

        rootVisualElement.Add(toolbar);
    }

    private void CreateGraphView()
    {
        _shapeEditorGraphView = new ShapeEditorGraphView(_shapeTemplate);
        _shapeTemplate.name = "Shape Editor Graph";
        rootVisualElement.Add(_shapeEditorGraphView);
    }
}