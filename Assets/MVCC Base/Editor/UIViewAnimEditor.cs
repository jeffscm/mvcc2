using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIViewAnim), true)]
public class UIViewAnimEditor : Editor
{
    UIViewAnim _instance;

    void OnEnable()
    {
        _instance = target as UIViewAnim;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        if (GUILayout.Button("Set Default OUT"))
        {
            _instance.SetDefaultOut();
        }
    }
}

