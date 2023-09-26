using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ToolUniformGridGenerator : MonoBehaviour
{
    [Header("Use ContextMenu to Generate")]
    public Transform gridMin;
    public Transform gridMax;
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
}
