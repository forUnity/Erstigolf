using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject ps;
    public float cooldown;
    public float ascencionHeight;
    public float ascencionTime;
    public float travelTime;
    public Image cooldownBar;
    public int currentArea;
    [Space]
    [SerializeField] private GolfkartSpeed golfkartSpeed;
    [SerializeField] private float speedToFly = 0f;
    //[SerializeField] private TMPro.TextMeshProUGUI 

    private float nextTP;

    private ButtonsInput inputs;
    private void Awake() {
        inputs = new ButtonsInput();
        inputs.Car.Blue.performed += x => TryUp();
        inputs.Car.White.performed += x => TryDown();

        if (ps){
            ps.SetActive(false);
        }
    }

    private void Update() {
        nextTP -= Time.deltaTime;
        if(speedToFly == 0f)
        {
            cooldownBar.fillAmount = 1-(nextTP/cooldown);
        } else
        {
            cooldownBar.fillAmount = golfkartSpeed.GetAvgSpeedOverTimeRecord() / speedToFly;
        }
        cooldownBar.color = cooldownBar.fillAmount >= 1 ? Color.green : Color.red;
    }

    private void OnEnable() {
        inputs.Enable();
    }

    private void OnDisable() {
        inputs.Disable();
    }

    private bool canTeleport => nextTP <= 0 && (golfkartSpeed ? golfkartSpeed.GetAvgSpeedOverTimeRecord() >= speedToFly : true);
    private void TryUp(){
        if (!canTeleport)
        {   
            AlertSystem.Message(nextTP > 0 ? "Flying Not Ready" : "Too Slow For Takeoff");
            return;
        }
        TeleportIncr();
    }

    private void TryDown(){
        if (!canTeleport)
        {
            AlertSystem.Message(nextTP > 0 ? "Flying Not Ready" : "Too Slow For Takeoff");
            return;
        }
        TeleportDecr();
    }

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

    private void TeleportTo(int newArea)
    {
        nextTP = cooldown;

        TeleportRect from = teleportAreas[currentArea];
        TeleportRect to = teleportAreas[newArea];
        float xFac = from.GetXFac(teleportTarget.position.x); 
        float yFac = from.GetYFac(teleportTarget.position.z);

        currentArea = newArea;
        Vector3 target = to.ClosestTeleportPoint(new Vector2(xFac, yFac));
        MoveTarget(target);
    }

    private async void MoveTarget(Vector3 target){
        if (ps){
            ps.SetActive(true);
        }
        Vector3 start = teleportTarget.position;
        Vector3 dir = target - start;
        dir.y = 0f;
        Vector3 look = teleportTarget.forward;
        Vector3 ascencionPoint = start;
        Vector3 ascencionTarget = target;
        ascencionPoint.y = ascencionHeight;
        ascencionTarget.y = ascencionHeight;
        float startTime = Time.time;
        while (startTime + ascencionTime > Time.time){
            float t = (Time.time - startTime)/ascencionTime;
            teleportTarget.position = Vector3.Lerp(start, ascencionPoint, t);
            teleportTarget.forward = Vector3.Slerp(look, dir, t);
            await System.Threading.Tasks.Task.Yield();
            if (!Application.isPlaying)
                return;
        }
        float highTime = Time.time;
        while (highTime + travelTime > Time.time){
            teleportTarget.forward = dir;
            teleportTarget.position = Vector3.Lerp(ascencionPoint, ascencionTarget, (Time.time - highTime)/travelTime);
            await System.Threading.Tasks.Task.Yield();
            if (!Application.isPlaying)
                return;
        }
        if (teleportTarget.TryGetComponent(out Rigidbody rb)){
            rb.velocity = Vector3.up * rb.velocity.y/2;
            rb.angularVelocity = Vector3.zero;
        }
        float rate = 0;
        if (ps){
            foreach(ParticleSystem ps in ps.GetComponentsInChildren<ParticleSystem>()){
                rate = ps.emission.rateOverDistance.constant;
                var em = ps.emission;
                var r = em.rateOverDistance;
                r.constant = 0;
                em.rateOverDistance = r;
            }
        }
        await System.Threading.Tasks.Task.Delay(1000);
        if (ps){
            ps.SetActive(false);
            foreach(ParticleSystem ps in ps.GetComponentsInChildren<ParticleSystem>()){
                var em = ps.emission;
                var r = em.rateOverDistance;
                r.constant = rate;
                em.rateOverDistance = r;
            }
        }
    }
}
