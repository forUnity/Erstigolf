using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ToolUniformGridGenerator : MonoBehaviour
{
    [Header("Use ContextMenu to Generate")]
    public Transform gridMin;
    public Transform gridMax;
    public Color debugColor = Color.blue;
    public float PointAxisDistance = 1f;
    public float yPositionOffset;
    [Space]
    public bool checkIntersection = true;
    public float checkSphereRad = 0.5f;
    public LayerMask checkLayerMask;
    //[Space]
    //public GameObject pointPrefab;
    [ContextMenu("Generate")]
    public void Generate()
    {
        float xMin = gridMin.position.x;
        float xMax = gridMax.position.x;
        float zMin = gridMin.position.z;
        float zMax = gridMax.position.z;
        for (float x = xMin; x <= xMax; x+=PointAxisDistance)
        {
            for (float z = zMin; z <= zMax; z += PointAxisDistance)
            {
                Vector3 pos = new Vector3(x, gridMin.position.y + yPositionOffset, z);
                if (checkIntersection && Intersects(pos))
                    continue;
                Transform t = new GameObject("grid " + x + " , " + z).transform;
                t.position = pos;
                t.parent = transform;
            }
        }
        Debug.Log("Done");
    }

    private bool Intersects(Vector3 pos) => Physics.CheckSphere(pos, checkSphereRad, checkLayerMask, QueryTriggerInteraction.Ignore);

    private void OnDrawGizmos() {

        for (int i = 0; i < transform.childCount; i++){
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.1f);
        }
        if (gridMax == null || gridMin == null)
            return;
        float xMin = gridMin.position.x;
        float xMax = gridMax.position.x;
        float zMin = gridMin.position.z;
        float zMax = gridMax.position.z;
        float y = gridMin.position.y + yPositionOffset;
        Vector3 v1 = new Vector3(xMin, y, zMin);
        Vector3 v2 = new Vector3(xMin, y, zMax);
        Vector3 v3 = new Vector3(xMax, y, zMax);
        Vector3 v4 = new Vector3(xMax, y, zMin);

        Gizmos.color = debugColor;
        Gizmos.DrawLine(v1, v2);
        Gizmos.DrawLine(v3, v2);
        Gizmos.DrawLine(v3, v4);
        Gizmos.DrawLine(v1, v4);
        Gizmos.DrawLine(v1, v3);
        Gizmos.DrawLine(v2, v4);
    }
}
