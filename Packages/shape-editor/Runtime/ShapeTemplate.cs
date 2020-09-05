using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Recgonizer/ShapeObject")]
public class ShapeTemplate : ScriptableObject
{
    public List<Vector2> points = new List<Vector2>();
}