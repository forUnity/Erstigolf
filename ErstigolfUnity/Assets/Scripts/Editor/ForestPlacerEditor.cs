using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ForestPlacer))]
public class ForestPlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ForestPlacer fp = (ForestPlacer)target;

        if (GUILayout.Button("Delete")){
            fp.Delete();
        }
        if (GUILayout.Button("Generate")){
            fp.PlaceProps();
        }
    }
}
