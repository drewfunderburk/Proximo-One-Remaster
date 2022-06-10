using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Wave
{
    [Tooltip("Enemy prefab to spawn")]
    public GameObject Prefab;
    [Tooltip("How many enemies to spawn")]
    public int Count;
    [Tooltip("How long to wait between enemy spawns")]
    public float SpawnDelay;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Wave)), CanEditMultipleObjects]
public class WavePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't indent child fields
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        float floatWidth = 35;
        float padding = 5;
        Rect countRect = new Rect(position.x, position.y, floatWidth, position.height);
        Rect spawnDelayRect = new Rect(position.x + position.width - floatWidth, position.y, floatWidth, position.height);
        Rect prefabRect = new Rect(position.x + countRect.width + padding, position.y, position.width - countRect.width - spawnDelayRect.width - (padding * 2), position.height);

        // Get properties
        SerializedProperty countProperty = property.FindPropertyRelative("Count");
        SerializedProperty prefabProperty = property.FindPropertyRelative("Prefab");
        SerializedProperty spawnDelayProperty = property.FindPropertyRelative("SpawnDelay");

        // Draw fields
        countProperty.intValue = EditorGUI.IntField(countRect, countProperty.intValue);
        EditorGUI.LabelField(countRect, new GUIContent("", "How many enemies to spawn"));

        EditorGUI.ObjectField(prefabRect, prefabProperty, GUIContent.none);
        EditorGUI.LabelField(prefabRect, new GUIContent("", "Prefab to spawn"));

        spawnDelayProperty.floatValue = EditorGUI.FloatField(spawnDelayRect, spawnDelayProperty.floatValue);
        EditorGUI.LabelField(spawnDelayRect, new GUIContent("", "Delay between prefab spawns"));

        // Reset indent
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
#endif