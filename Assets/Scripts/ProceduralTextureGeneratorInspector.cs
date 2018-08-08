using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProceduralTextureGenerator))]
public class ProceduralTextureGeneratorInspector : Editor
{
    private ProceduralTextureGenerator generator;

    private void OnEnable()
    {
        generator = target as ProceduralTextureGenerator;
        Undo.undoRedoPerformed += RefreshGenerator;
    }

    private void OnDisable()
    {
        Undo.undoRedoPerformed -= RefreshGenerator;
    }

    private void RefreshGenerator()
    {
        if (Application.isPlaying)
        {
            generator.generationMethod();
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck())
        {
            RefreshGenerator();
        }
    }
}