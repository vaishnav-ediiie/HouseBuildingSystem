using BuildSystemSpace;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BuildProbSO))]
public class ProbEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BuildProbSO bso = (BuildProbSO)target;
        EditorGUI.BeginChangeCheck();
        
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}