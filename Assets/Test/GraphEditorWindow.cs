using UnityEngine;
using UnityEditor;
using static UnityEditor.PlayerSettings;

public class GraphEditorWindow : EditorWindow {
    private GraphAsset graph;
    private int selectedNode = -1;
    private bool addNeighborState = false;
    private Vector2 dragOffset;
    private float gridSize = 100f; // шаг сетки

    private bool canDrag = false; // шаг сетки


    [MenuItem("Window/Graph Editor")]
    static void OpenWindow() {
        GetWindow<GraphEditorWindow>("Graph Editor");
    }

    private void OnGUI() {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(GUILayout.Width(200));

        graph = (GraphAsset) EditorGUILayout.ObjectField("Graph", graph, typeof(GraphAsset), false);

        if (graph != null) {
            if (GUILayout.Button("Добавить вершину")) {
                Undo.RecordObject(graph, "Add Node");
                graph.nodes.Add(new GraphNode { position = new Vector2(gridSize, gridSize) });
                EditorUtility.SetDirty(graph);
            }
        }

        GUILayout.EndVertical();

        if (graph == null) {
            GUILayout.EndHorizontal();
            return;
        }

        Rect canvasRect = GUILayoutUtility.GetRect(1000, 800);
        GUI.Box(canvasRect, "");

        Event e = Event.current;

        Rect menuRect = new Rect(0f, 0f, 0f, 0f);

        // Управление выделенной нодой
        if (selectedNode >= 0 && selectedNode < graph.nodes.Count) {
            menuRect = new Rect(canvasRect.width + 300f, canvasRect.yMax + 10, 300, 150);

            GUILayout.BeginArea(menuRect, "Node Inspector", GUI.skin.window);

            GUILayout.Label($"Node {selectedNode}");
            if (GUILayout.Button("Удалить вершину")) {
                Undo.RecordObject(graph, "Remove Node");
                graph.nodes.RemoveAt(selectedNode);
                selectedNode = -1;
                EditorUtility.SetDirty(graph);
            }

            addNeighborState = EditorGUILayout.Toggle("Добавить соседа", addNeighborState);

            if (GUILayout.Button("Закрыть")) {
                selectedNode = -1;
                addNeighborState = false;
            }

            GUILayout.EndArea();
        }



        // Drag с привязкой к сетке
        if (selectedNode >= 0 && !menuRect.Contains(e.mousePosition)) {
            Vector2 pos = graph.nodes[selectedNode].position;
            Rect nodeRect = new Rect(pos - Vector2.one * 15, new Vector2(30, 30));

            if (e.type == EventType.MouseDown && e.button == 0 && nodeRect.Contains(e.mousePosition)) {
                canDrag = true;
            }

            if (e.type == EventType.MouseUp) {
                canDrag = false;
            }

            Debug.Log("canDrag " + canDrag);
            if (canDrag && e.type == EventType.MouseDrag) {

                Vector2 rawPos = e.mousePosition + dragOffset;

                // Привязка к сетке
                float x = Mathf.Round(rawPos.x / gridSize) * gridSize;
                float y = Mathf.Round(rawPos.y / gridSize) * gridSize;

                graph.nodes[selectedNode].position = new Vector2(x, y);
                Repaint();
            }
        }

        //if (selectedNode >= 0 && e.type == EventType.MouseUp) {
        //    Undo.RecordObject(graph, "Move Node");
        //    EditorUtility.SetDirty(graph);
        //    Vector2 pos = graph.nodes[selectedNode].position;
        //    Rect nodeRect = new Rect(pos - Vector2.one * 15, new Vector2(30, 30));
        //
        //    if (!nodeRect.Contains(e.mousePosition)) {
        //        selectedNode = -1;
        //    }
        //}



        // Рисуем связи
        Handles.BeginGUI();
        for (int i = 0; i < graph.nodes.Count; i++) {
            foreach (var neighbor in graph.nodes[i].neighbors) {
                if (neighbor < graph.nodes.Count)
                    Handles.DrawLine(graph.nodes[i].position, graph.nodes[neighbor].position);
            }
        }
        Handles.EndGUI();


        // Рисуем вершины
        for (int i = 0; i < graph.nodes.Count; i++) {
            Vector2 pos = graph.nodes[i].position;
            Rect nodeRect = new Rect(pos - Vector2.one * 15, new Vector2(30, 30));

            // Ловим нажатие мыши
            if (e.type == EventType.MouseDown && e.button == 0 && nodeRect.Contains(e.mousePosition)) {
                if (selectedNode == i) {
                    selectedNode = -1;
                }
                else if (selectedNode == -1) {
                    selectedNode = i;
                }
                else {
                    if (addNeighborState) {
                        if (!graph.nodes[selectedNode].neighbors.Contains(i)) {
                            graph.nodes[selectedNode].neighbors.Add(i);
                            graph.nodes[i].neighbors.Add(selectedNode);
                        }
                        else {
                            graph.nodes[selectedNode].neighbors.Remove(i);
                            graph.nodes[i].neighbors.Remove(selectedNode);
                        }
                    }
                    else {
                        selectedNode = i;
                    }
                }
                dragOffset = graph.nodes[i].position - e.mousePosition;
                e.Use();
            }
            GUIStyle style = new GUIStyle(GUI.skin.button);
            if (i == selectedNode) style.normal.textColor = Color.red;

            GUI.Button(nodeRect, i.ToString(), style);
        }

        GUILayout.EndHorizontal();
    }
}