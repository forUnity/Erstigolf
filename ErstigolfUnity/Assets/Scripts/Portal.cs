using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Mesh previewMesh;

    private void OnTriggerEnter(Collider other) {
        PlayerCar pc = other.GetComponentInParent<PlayerCar>();
        if (pc){
            pc.transform.position = target.position;
            pc.transform.rotation = target.rotation;
        }
    }

    private void OnDrawGizmos() {
        if (target && previewMesh) {
            Gizmos.color = Color.red;
            Gizmos.DrawMesh(previewMesh, target.position, target.rotation);
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
