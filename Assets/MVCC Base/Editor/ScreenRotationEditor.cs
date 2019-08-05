using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScreenRotation), true)]
public class ScreenRotationEditor : Editor
{
    ScreenRotation _instance;

    void OnEnable()
    {
        _instance = target as ScreenRotation;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        if (GUILayout.Button("Fire Rotation"))
        {
            ScreenRotation.OnRotationChange?.Invoke(_instance.testScreenRotation);
        }
    }

}
