using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ShapeManipulator : MouseManipulator
{
    private ShapeTemplate _shapeTemplate;
    private ShapeDraw _shapeDraw;

    public ShapeManipulator(ShapeTemplate shapeTemplate)
    {
        activators.Add(new ManipulatorActivationFilter {button = MouseButton.LeftMouse});
        _shapeTemplate = shapeTemplate;
        _shapeDraw = new ShapeDraw {Points = shapeTemplate.points};
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.Add(_shapeDraw);
        target.Add(new Label {name = "mousePosition", text = "(0,0)"});
        target.RegisterCallback<MouseDownEvent>(MouseDown);
        target.RegisterCallback<MouseMoveEvent>(MouseMove);
        target.RegisterCallback<MouseCaptureOutEvent>(MouseOut);
        target.RegisterCallback<MouseUpEvent>(MouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(MouseDown);
        target.UnregisterCallback<MouseUpEvent>(MouseUp);
        target.UnregisterCallback<MouseMoveEvent>(MouseMove);
        target.UnregisterCallback<MouseCaptureOutEvent>(MouseOut);
    }

    private void MouseOut(MouseCaptureOutEvent evt) => _shapeDraw.DrawSegment = false;

    private void MouseMove(MouseMoveEvent evt)
    {
        var t = target as ShapeEditorGraphView;
        var mouseLabel = target.Q("mousePosition") as Label;
        mouseLabel.transform.position = evt.localMousePosition + Vector2.up * 20;
        mouseLabel.text = t.ScreenToWorldSpace(evt.localMousePosition).ToString();

        // When left mouse button is pressed... 
        if ((evt.pressedButtons & 1) != 1)
        {
            return;
        }

        _shapeDraw.End = t.ScreenToWorldSpace(evt.localMousePosition);
        _shapeDraw.MarkDirtyRepaint();
    }

    private void MouseUp(MouseUpEvent evt)
    {
        if (!CanStopManipulation(evt))
        {
            return;
        }

        target.ReleaseMouse();
        if (!_shapeDraw.DrawSegment)
        {
            return;
        }

        if (_shapeTemplate.points.Count == 0)
        {
            _shapeTemplate.points.Add(_shapeDraw.Start);
        }

        var t = target as ShapeEditorGraphView;
        _shapeTemplate.points.Add(t.ScreenToWorldSpace(evt.localMousePosition));
        _shapeDraw.DrawSegment = false;

        _shapeDraw.MarkDirtyRepaint();
    }

    private void MouseDown(MouseDownEvent evt)
    {
        if (!CanStartManipulation(evt)) return;
        target.CaptureMouse();

        _shapeDraw.DrawSegment = true;
        var t = target as ShapeEditorGraphView;

        if (_shapeTemplate.points.Count != 0)
        {
            _shapeDraw.Start = _shapeTemplate.points.Last();
        }
        else
        {
            _shapeDraw.Start = t.ScreenToWorldSpace(evt.localMousePosition);
        }

        _shapeDraw.End = t.ScreenToWorldSpace(evt.localMousePosition);
        _shapeDraw.MarkDirtyRepaint();
    }

    private class ShapeDraw : ImmediateModeElement
    {
        public List<Vector2> Points { get; set; } = new List<Vector2>();
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }
        public bool DrawSegment { get; set; }

        protected override void ImmediateRepaint()
        {
            var lineColor = new Color(1.0f, 0.6f, 0.0f, 1.0f);
            var t = parent as ShapeEditorGraphView;

            for (var i = 0; i < Points.Count - 1; i++)
            {
                var p1 = t.WorldtoScreenSpace(Points[i]);
                var p2 = t.WorldtoScreenSpace(Points[i + 1]);
                GL.Begin(GL.LINES);
                GL.Color(lineColor);
                GL.Vertex(p1);
                GL.Vertex(p2);
                GL.End();
            }

            if (!DrawSegment)
            {
                return;
            }

            GL.Begin(GL.LINES);
            GL.Color(lineColor);
            GL.Vertex(t.WorldtoScreenSpace(Start));
            GL.Vertex(t.WorldtoScreenSpace(End));
            GL.End();
        }
    }
}