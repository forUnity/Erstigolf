using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetDrawer : MonoBehaviour
{
    public static StreetDrawer Instance;

    public Camera drawCam;
    public GameObject drawBrushPrefab;
    public GameObject streetPrefab;
    public float minSegmentLength = 1f;
    public float AsphaltDistanceAtFull = 100f;
    public float remainingAsphalt;

    private bool isDrawing;
    private GameObject brushObject;
    private List<Vector3> path = new List<Vector3>();
    private float drawnLengthSoFar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        if (drawCam == null)
        {
            Debug.LogWarning("No camera set for drawing! Using main camera instead", gameObject);
            drawCam = Camera.main;
        }
        remainingAsphalt = AsphaltDistanceAtFull;

        // add colliders to all existing streets
        foreach (StreetCurve street in FindObjectsOfType<StreetCurve>())
        {
            street.GenerateMeshCollider();
        }
    }

    private void Update()
    {
        if (isDrawing &&
            Input.GetMouseButtonUp(0)
            && IsMouseInTopdownViewport())
        {
            FinishDrawing();
        }
        else if (isDrawing && (!IsMouseInTopdownViewport() || Input.GetMouseButtonDown(1)))
        {
            CancelDrawing();
        }
        else if (!isDrawing && Input.GetMouseButtonDown(0) && IsMouseInTopdownViewport())
        {
            StartDrawing();
        }
        else if (isDrawing)
        {
            ContinueDrawing();
        }
    }

    private void StartDrawing()
    {
        isDrawing = true;
        brushObject = Instantiate(drawBrushPrefab, GetTargetedPosition(), Quaternion.identity);
        path.Clear();
        path.Add(brushObject.transform.position);
        drawnLengthSoFar = 0;
    }

    private void FinishDrawing()
    {
        if (path.Count <= 1) // odd curve? just cancel instead
        {
            CancelDrawing();
            return;
        }
        isDrawing = false;
        Destroy(brushObject);
        GameObject street = Instantiate(streetPrefab);
        StreetCurve curve = street.GetComponent<StreetCurve>();
        curve.SetWorldPositions(path.ToArray());
        curve.numberOfSamples = Mathf.CeilToInt(drawnLengthSoFar + 1);
        curve.FlattenCurve();
        curve.SmoothCurve();
        curve.GenerateMeshCollider();

        remainingAsphalt -= drawnLengthSoFar;
        AsphaltBar.Instance.SetActuallyLeft(remainingAsphalt / AsphaltDistanceAtFull);
    }

    private void CancelDrawing()
    {
        isDrawing = false;
        Destroy(brushObject);
        AsphaltBar.Instance.ResetIndicatedLeft();
    }

    private void ContinueDrawing()
    {
        brushObject.transform.position = GetTargetedPosition();
        float addedDistance = (path[path.Count - 1] - brushObject.transform.position).magnitude;
        if (remainingAsphalt - (drawnLengthSoFar + addedDistance) < 0)
        {
            CancelDrawing();
            return;
        }

        if (addedDistance > minSegmentLength)
        {
            path.Add(brushObject.transform.position);
            drawnLengthSoFar += addedDistance;
            AsphaltBar.Instance.SetIndicatedLeft((remainingAsphalt - drawnLengthSoFar) / AsphaltDistanceAtFull);
        }

    }

    private Vector3 GetTargetedPosition()
    {
        Ray drawRay = drawCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(drawRay, out RaycastHit hit))
        {
            return hit.point + Vector3.up * 0.2f;
        }
        else
        {
            Debug.LogWarning("Raycast failed during drawing", gameObject);
            return new Vector3(drawRay.origin.x, 0, drawRay.origin.z);
        }
    }

    private bool IsMouseInTopdownViewport()
    {
        return drawCam.pixelRect.Contains(Input.mousePosition);
    }


    public void AddAsphalt(float distance)
    {
        remainingAsphalt += distance;
        remainingAsphalt = Mathf.Min(remainingAsphalt, AsphaltDistanceAtFull);
        AsphaltBar.Instance.SetActuallyLeft(remainingAsphalt / AsphaltDistanceAtFull);
    }
}
