using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake() {
        ps = GetComponent<ParticleSystem>();
        if (!ps) Destroy(gameObject);
        ps.Play();
    }

    private void Update() {
        if (!ps.isPlaying){
            Destroy(gameObject);
        }
    }
}
