using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UI3DView), true)]
public class UI3DViewEditor : Editor
{

    UI3DView _instance;

    SerializedProperty controllerId;
    SerializedProperty navAnimate;

    void OnEnable()
    {
        _instance = target as UI3DView;

        controllerId = serializedObject.FindProperty("controllerId");
        navAnimate = serializedObject.FindProperty("navAnimate");
        CheckParent();

        var checkNavAnimate = _instance.GetComponent<Nav3DAnimate>();
        if (checkNavAnimate == null)
        {
            var a = _instance.gameObject.AddComponent<Nav3DAnimate>();
            _instance.navAnimate = a;
        }

    }

    void CheckParent()
    {
        var p = _instance.transform.parent;

        while(p != null)
        {
            var src = p.GetComponent<UIViewControllerBase>();
            if (src != null)
            {
                controllerId.longValue = src.controllerId;
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
                return;
            }

            p = p.transform.parent;
        }

        controllerId.longValue = -1;
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("Controller ID:", controllerId.longValue.ToString());
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(navAnimate);
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Custom Fields:");
        DrawDefaultInspector();

    }
}

