using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AppMonoController), true)]
public class AppMonoControllerEditor : Editor
{

    SerializedProperty controllerId;

    void OnEnable()
    {
        controllerId = serializedObject.FindProperty("controllerId");
        if (controllerId.longValue <= 0)
        {
            controllerId.longValue = System.DateTime.Now.Ticks;
            serializedObject.ApplyModifiedProperties();
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //EditorGUILayout.PropertyField(controllerId);

        EditorGUILayout.LabelField("Controller ID:", controllerId.longValue.ToString());
        
        serializedObject.ApplyModifiedProperties();

        if (controllerId.longValue <= 0)
        {
            controllerId.longValue = System.DateTime.Now.Ticks;
            serializedObject.ApplyModifiedProperties();
        }

        DrawDefaultInspector();
    }
}


public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}