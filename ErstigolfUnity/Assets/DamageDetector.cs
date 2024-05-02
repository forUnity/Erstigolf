using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    public Transform hoverT;
    public float hoverAmplitude = 0.1f;
    public float hoverSpeed = 1f;
    public float hoverHeight = 1f;

    public LayerMask groundLayer;


    //hunt player
    public Transform playerTarget;
    public float minSpeed = 2f;
    public float maxSpeed = 50f;


    public float boostUpVelcoity = 30f;

    public PlayRandomSound attackSounds;
    public PlayRandomSound idleSounds;
    public AudioSource defeatSound;

    void LateUpdate() {
        transform.LookAt(playerTarget);
    
        RaycastHit hit;
        Vector3 sheepToPlayer = playerTarget.position - transform.position;

        float distanceSpeed = Mathf.Clamp(sheepToPlayer.magnitude, minSpeed, maxSpeed);

        Vector3 pos = transform.position + sheepToPlayer.normalized * distanceSpeed * Time.deltaTime;
        if(false && Physics.Raycast(hoverT.position, Vector3.down, out hit, 100f, groundLayer)) {
            pos.y = hit.point.y + Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude + hoverHeight;
            Debug.DrawRay(hoverT.position, Vector3.up * hit.distance, Color.red);
        }
        else {pos.y = playerTarget.position.y + Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude + hoverHeight;}
    
        transform.position = pos;

        idleSounds?.TryPlayRandomSound();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");

        //check if the object we collided with has a Pizza GetComponent
        Pizza pizza =other.GetComponent<Pizza>();
        if (pizza != null)
        {
            defeatSound.Play();
            defeatSound.transform.SetParent(null);
            Destroy(defeatSound.gameObject, defeatSound.clip.length);

            Destroy(gameObject);
            return;
        }

        //check if is player
        
        if(other.GetComponentInParent<Rigidbody>() != null)
        {
            Debug.Log("Player hit");
            other.GetComponentInParent<Rigidbody>().velocity = Vector3.up * boostUpVelcoity;
        }
        attackSounds?.TryPlayRandomSound();
    } 
}
