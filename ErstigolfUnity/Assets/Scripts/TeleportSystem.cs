using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TeleportSystem : MonoBehaviour
{
    [System.Serializable]
    public class TeleportRect
    {
        public string Name = "Area";
        public Transform lowerCorner;
        public Transform upperCorner;

        public Transform[] TeleportPoints;//uniform grid of transforms with possible teleport arrival points -> no tping inside buildings
 
        public Vector3 ClosestTeleportPoint(Vector2 facCoords)
        {
            float minDist = float.MaxValue;
            Vector3 minPos = Vector3.zero;
            foreach (var t in TeleportPoints)
            {
                float d = (GetFactorCoords(t.position.x, t.position.z) - facCoords).sqrMagnitude;
                if(d < minDist)
                {
                    minDist = d;
                    minPos = t.position;
                }
            }
            return minPos;
        }

        public Vector2 GetFactorCoords(float targetX, float targetY) => new Vector2(GetXFac(targetX), GetYFac(targetY));

        public float WidthX => upperCorner.position.x - lowerCorner.position.x;
        public float WidthY => upperCorner.position.z - lowerCorner.position.z;

        public float GetXFac(float targetX) => Mathf.Clamp01((targetX - lowerCorner.position.x) / WidthX);
        public float GetYFac(float targetY) => Mathf.Clamp01((targetY - lowerCorner.position.z) / WidthY);

        public float GetNewX(float xFac) => lowerCorner.position.x + WidthX * xFac;
        public float GetNewY(float yFac) => lowerCorner.position.z + WidthY * yFac;

    }
    public TeleportRect[] teleportAreas;

    [Space]
    public Transform teleportTarget;
    public int currentArea;

    #region Input
    [ContextMenu("TeleportIncr")]
    public void TeleportIncr()
    {
        TeleportTo((currentArea + 1) % teleportAreas.Length);
    }
    [ContextMenu("TeleportDecr")]
    public void TeleportDecr()
    {
        TeleportTo(currentArea > 0 ? currentArea - 1 : teleportAreas.Length-1);
    }
    #endregion

    [ContextMenu("Take Points")]
    private void TakePoints(){
        foreach (TeleportRect r in teleportAreas){
            Transform holder = r.lowerCorner.parent;
            r.TeleportPoints = new Transform[holder.childCount - 2];
            for (int i = 2; i < holder.childCount; i++){
                r.TeleportPoints[i-2] = holder.GetChild(i);
            }
        }
        Debug.Log("done");
    }

    public void TeleportTo(int newArea)
    {
        TeleportRect from = teleportAreas[currentArea];
        TeleportRect to = teleportAreas[newArea];
        float xFac = from.GetXFac(teleportTarget.position.x); 
        float yFac = from.GetYFac(teleportTarget.position.z);

        //float newXPos = to.GetNewX(xFac);
        //float newYPos = to.GetNewY(yFac);

        teleportTarget.position = to.ClosestTeleportPoint(new Vector2(xFac, yFac));
        //PlayerCar.teleportEffect() //cam black; tires
        currentArea = newArea;
    }
}
