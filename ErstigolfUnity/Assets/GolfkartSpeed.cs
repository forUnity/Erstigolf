using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfkartSpeed : MonoBehaviour
{
    public Transform trackSpeedT;
    public int AvgPositionsCount = 3;
    public float RecordPositionDelay = 0.1f;
    private List<(Vector3, float)> PosAtTimeRecord = new List<(Vector3, float)>();

    float lastRecordTime = 0f;
    void Update()
    {
        if(Time.time - lastRecordTime >= RecordPositionDelay)
        {
            PosAtTimeRecord.Add((trackSpeedT.position, Time.time));
            lastRecordTime = Time.time;
            cacheDirty = true;
            if(PosAtTimeRecord.Count > AvgPositionsCount)
            {
                PosAtTimeRecord.RemoveRange(0, PosAtTimeRecord.Count-AvgPositionsCount);
            }
        }
    }

    float cacheSpeedInMPerS;
    bool cacheDirty;
    public float GetAvgSpeedOverTimeRecord()
    {
        if (cacheDirty)
        {
            float velocitySum = 0f;

            for (int i = 1; i < PosAtTimeRecord.Count; i++)
            {
                float dist = (PosAtTimeRecord[i].Item1 - PosAtTimeRecord[i-1].Item1).magnitude;
                velocitySum += dist / (PosAtTimeRecord[i].Item2 - PosAtTimeRecord[i - 1].Item2);
            }

            cacheSpeedInMPerS = velocitySum / (PosAtTimeRecord.Count > 1 ? PosAtTimeRecord.Count - 1 : 1);
        }

        cacheDirty = false;
        return cacheSpeedInMPerS;
    }
}
