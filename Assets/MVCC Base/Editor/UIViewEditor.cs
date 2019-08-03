using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIView), true)]
public class UIViewEditor : Editor
{

    UIView _instance;

    SerializedProperty controllerId;
    SerializedProperty navAnimate;

    void OnEnable()
    {
        _instance = target as UIView;

        controllerId = serializedObject.FindProperty("controllerId");
        navAnimate = serializedObject.FindProperty("navAnimate");
        CheckParent();

        var checkNavAnimate = _instance.GetComponent<NavAnimate>();
        if (checkNavAnimate == null)
        {
            var a = _instance.gameObject.AddComponent<NavAnimate>();
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

