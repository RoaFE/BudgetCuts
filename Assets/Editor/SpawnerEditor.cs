using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor {
    Spawner m_target;
    public override void OnInspectorGUI() {
        m_target = (Spawner)target;
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Path points");
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Add"))
        {
            m_target.Add();
        }
        if(GUILayout.Button("Remove"))
        {
            m_target.Remove();
        }
        EditorGUILayout.EndHorizontal();
        for(int i = 0; i < m_target.Points.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField($"Point{i + 1}: ",GUILayout.Width(100));
            m_target.Points[i] = EditorGUILayout.Vector3Field($"Point{i + 1}:", m_target.Points[i]);
            EditorGUILayout.EndHorizontal();
        }

        if(GUILayout.Button("Clear"))
        {
            m_target.Reset();
        }
    }

    public void OnSceneGUI()
    {
        EditorGUI.BeginChangeCheck();
        if(m_target.Points == null)
        {

        }
        for (int i = 0; i < m_target.Points.Count; i++)
        {
            Vector3 pos = Handles.PositionHandle(m_target.Points[i], Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Move point");
                m_target.Points[i] = pos;

            }
            if(i < m_target.Points.Count - 1)
            {
                Handles.DrawLine(m_target.Points[i], m_target.Points[i + 1]);
            }
        }

    }
}