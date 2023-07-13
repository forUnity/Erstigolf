using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownPlayerMoveCameraCenter : MonoBehaviour
{
    [SerializeField] private float camTargetMoveSpeedUnitsPerSec = 10f;
    private void Update()
    {
        float xAxis = (Input.GetKey(KeyCode.RightArrow) ? 1f : (Input.GetKey(KeyCode.LeftArrow)? -1f : 0f));
        float yAxis = (Input.GetKey(KeyCode.UpArrow) ? 1f : (Input.GetKey(KeyCode.DownArrow) ? -1f : 0f));

        //transform.position += new Vector3(xAxis, 0f, yAxis).normalized * camTargetMoveSpeedUnitsPerSec * Time.deltaTime;
        transform.position += (transform.right * xAxis + transform.forward * yAxis).normalized * camTargetMoveSpeedUnitsPerSec * Time.deltaTime;
    }
}
