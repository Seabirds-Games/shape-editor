
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ShapeEditorGraphView : GraphView
{
    
    const float PixelsPerUnit = 100f;
    const bool InvertYPosition = true;
    
    public ShapeEditorGraphView(ShapeTemplate shapeTemplate)
    {
        styleSheets.Add(Resources.Load<StyleSheet>("ShapeEditorGraph"));
        this.StretchToParentSize();
        
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        Add(new GridBackground());
        
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new ShapeManipulator(shapeTemplate));
        
        contentViewContainer.BringToFront();
        contentViewContainer.Add(new Label { name = "origin", text = "(0,0)"});

        schedule.Execute(() =>
        {
            contentViewContainer.transform.position = parent.localBound.size / 2f;
        });
    }

    public Vector2 WorldtoScreenSpace(Vector2 pos)
    {
        var position = pos * PixelsPerUnit - contentViewContainer.layout.position;
        if (InvertYPosition)
        {
            position.y = -position.y;
        } 
        return contentViewContainer.transform.matrix.MultiplyPoint3x4(position);        
    }

    public Vector2 ScreenToWorldSpace(Vector2 pos)
    {             
        Vector2 position = contentViewContainer.transform.matrix.inverse.MultiplyPoint3x4(pos);
        if (InvertYPosition)
        {
            position.y = -position.y;
        }        
        return (position + contentViewContainer.layout.position) / PixelsPerUnit;
    }
    
}