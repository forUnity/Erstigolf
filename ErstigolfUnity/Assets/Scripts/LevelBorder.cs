using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBorder : MonoBehaviour
{
    private void OnDrawGizmos() {
        if (TryGetComponent(out BoxCollider box)){
            Gizmos.color = new Color(1, 0, 1, 0.5f);
            Gizmos.DrawMesh(Resources.GetBuiltinResource<Mesh>("Cube.fbx"), transform.position, transform.rotation, box.size);
        }
    }
}
