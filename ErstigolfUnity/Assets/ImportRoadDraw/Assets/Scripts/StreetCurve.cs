using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetCurve : MonoBehaviour
{
    [Header("Smoothing")]
    public int numberOfSamples = 100;
    [Header("Subdividing")]
    public float maxLengthOfSegments = 2f;

    [ContextMenu("Flatten curve to ground")]
    public void FlattenCurve()
    {
        Vector3[] positions = GetWorldPositions();

        List<Vector3> newPositions = new List<Vector3>();

        // snap to ground
        for (int i = 0; i < positions.Length; i++)
        {
            if (Physics.Raycast(positions[i] + Vector3.up * 100, Vector3.down, out RaycastHit hit))
            {
                positions[i] = hit.point;
                if (i != 0 && i != positions.Length - 1) positions[i] += Vector3.up * 0.1f;
            }
        }
        SetWorldPositions(positions);
    }

    [ContextMenu("Smooth Curve")]
    public void SmoothCurve()
    {
        Vector3[] positions = GetWorldPositions();
        Spliner sp = new Spliner();
        sp.SetPoints(positions);
        int n = 100;
        Vector3[] smoothed = new Vector3[n];
        for (int i = 0; i < n; i++)
        {
            smoothed[i] = sp.Sample((float)i / (n - 1));
        }

        SetWorldPositions(smoothed);
    }

    [ContextMenu("Subdivide Curve without smoothing")]
    public void SubdivideCurve()
    {
        Vector3[] positions = GetWorldPositions();

        List<Vector3> subdividedPositions = new List<Vector3>();

        for (int i = 0; i < positions.Length - 1; i++)
        {
            float segmentLength = (positions[i + 1] - positions[i]).magnitude;
            int subdivisions = Mathf.CeilToInt(segmentLength / maxLengthOfSegments);

            subdividedPositions.Add(positions[i]);
            for (int j = 1; j < subdivisions; j++)
            {
                subdividedPositions.Add(Vector3.Lerp(positions[i], positions[i + 1], j / (float)subdivisions));
            }
        }
        subdividedPositions.Add(positions[positions.Length - 1]);
        SetWorldPositions(subdividedPositions.ToArray());
    }

    [ContextMenu("Generate MeshCollider")]
    public void GenerateMeshCollider()
    {
        MeshCollider meshCol = GetComponent<MeshCollider>();
        if (meshCol == null) meshCol = gameObject.AddComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        GetComponent<LineRenderer>().BakeMesh(mesh, useTransform: false);
        meshCol.sharedMesh = mesh;
    }

    public Vector3[] GetWorldPositions()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = lr.localToWorldMatrix.MultiplyPoint(positions[i]);
        }
        return positions;
    }

    public void SetWorldPositions(Vector3[] positions)
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = positions.Length;
        for (int i = 0; i < positions.Length; i++)
        {
            lr.SetPosition(i, lr.worldToLocalMatrix.MultiplyPoint(positions[i]));
        }
    }


    public class Spliner
    {
        private Vector3[] points;

        private float[] lengthPerSegment;
        private float totalLength;

        public void SetPoints(Vector3[] points)
        {
            this.points = points;
            lengthPerSegment = new float[points.Length - 1];
            totalLength = 0;
            for (int i = 0; i < points.Length-1; i++)
            {
                float dist = (points[i] - points[i + 1]).magnitude;
                lengthPerSegment[i] = dist;
                totalLength += dist;
            }
        }

        public Vector3 Sample(float t)
        {
            // handle t=1 explicitly to avoid out-of-bounds later
            if (t == 1f)
            {
                return points[points.Length - 1];
            }

            float tDist = t * totalLength;

            // find relevant section
            int from = 0;
            float lenSoFar = 0;
            while (lenSoFar + lengthPerSegment[from] <= tDist)
            {
                lenSoFar += lengthPerSegment[from];
                from++;
            }
            int to = from + 1;

            // sample within this segment
            tDist -= lenSoFar;
            t = tDist / lengthPerSegment[from];
            if (t < 0 || t > 1) Debug.LogError(t + " " + lengthPerSegment + " " + tDist);
            // actually sample
            Vector3 fromForward = from == 0 ? Vector3.zero : (points[from + 1] - points[from - 1]).normalized;
            Vector3 toBackwards = to == points.Length-1 ? Vector3.zero : (points[to - 1] - points[to + 1]).normalized;

            Vector3 fromHandleInterp = points[from] + fromForward * lengthPerSegment[from]/2f * t;
            Vector3 toHandleInterp = points[to] + toBackwards * lengthPerSegment[from]/2f * (1f-t);
            return Vector3.Lerp(fromHandleInterp, toHandleInterp, t);
        }
    }


}
