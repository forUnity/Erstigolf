using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Prop))]
public class PropDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var prefabRect = new Rect(position.x - 40, position.y, 150, position.height);
        EditorGUI.PropertyField(prefabRect, property.FindPropertyRelative("prefab"), GUIContent.none);

        var amountRect = new Rect(position.x + 115, position.y, 80, position.height);
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("relativeAmount"), GUIContent.none);
        
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
