using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomSound : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
   public void TryPlayRandomSound() {
        if(audioSource.isPlaying) return;

        audioSource.clip = sounds[Random.Range(0, sounds.Length)];
        audioSource.Play();
    }
  
}
