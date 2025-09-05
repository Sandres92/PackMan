using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraphNode {
    public Vector2 position;
    public List<int> neighbors = new List<int>();
}
