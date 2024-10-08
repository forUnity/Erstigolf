using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollower : MonoBehaviour
{
    public Transform ObjectToFollow;
    public bool FollowPosition = true;
    public bool FollowRotation = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowPosition) {
            transform.position = new Vector3 (ObjectToFollow.position.x, transform.position.y, ObjectToFollow.position.z);
        }

        if (FollowRotation) {
            transform.rotation = Quaternion.Euler(90, ObjectToFollow.rotation.eulerAngles.y, 0);
        }
    }
}
