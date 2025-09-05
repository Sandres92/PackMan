using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Graph/GraphAsset")]
public class GraphAsset : ScriptableObject {
    public List<GraphNode> nodes = new List<GraphNode>();
}