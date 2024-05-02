using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpiGlowAnimation : MonoBehaviour
{
   [SerializeField] private Material material;
   [SerializeField] private float S  = 1f;
    [SerializeField] private float V = 1f;

[SerializeField] private float intensity = 1f;
    [SerializeField] private float speed = 1f;

    void Update()
    {
        //change emmision color hue over time in circle
        float hue = (Time.time * speed) % 1;
        material.SetColor("_EmissionColor", Color.HSVToRGB(hue, S, V) * intensity);
    }
   
}
