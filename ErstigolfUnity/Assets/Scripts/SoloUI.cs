using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloUI : MonoBehaviour
{
    [SerializeField] AlertSystem alertSystem;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject[] deactivatedObjects;

    private void Start() {
        if (PlayerPrefs.HasKey("SoloMode") && PlayerPrefs.GetInt("SoloMode") == 1){
            for (int i = 0; i < deactivatedObjects.Length; i++){
                deactivatedObjects[i].SetActive(false);
            }
            alertSystem.enabled = false;
            canvas.targetDisplay = 0;
        }
        else {
            enabled = false;
        }
    }
}
