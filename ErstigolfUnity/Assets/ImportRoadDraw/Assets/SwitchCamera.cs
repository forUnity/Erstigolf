using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject vcam1Obj;
    [SerializeField] private MonoBehaviour[] vcam1Comps;
    [SerializeField] private GameObject vcam2Obj;
    [SerializeField] private MonoBehaviour[] vcam2Comps;
    //[SerializeField] private GameObject vcam3Obj;
    //[SerializeField] private MonoBehaviour[] vcam3Comps;

    [SerializeField] private Transform cam1TargetT;
    [SerializeField] private Transform playerT;

    [SerializeField] KeyCode switchKey;

    int currentCam1;
    private void Start()
    {
        SwitchCam(1);
    }
    private void Update()
    {
        if(Input.GetKeyDown(switchKey))
        {
            SwitchCam((currentCam1+1)%2);
        }
    }

    void SwitchCam(int cam)
    {
        if(cam == 0)
        {
            cam1TargetT.position = new Vector3(playerT.position.x, cam1TargetT.position.y, playerT.position.z);
        }

        vcam1Obj?.SetActive(cam == 0);
        vcam2Obj?.SetActive(cam == 1);
        //vcam2Obj?.SetActive(cam == 2);

        foreach (var item in vcam1Comps)
        {
            item.enabled = cam == 0;
        }
        foreach (var item in vcam2Comps)
        {
            item.enabled = cam == 1;
        }
        //foreach (var item in vcam3Comps)
        //{
        //    item.enabled = cam == 2;
        //}
        currentCam1 = cam;
    }
}
