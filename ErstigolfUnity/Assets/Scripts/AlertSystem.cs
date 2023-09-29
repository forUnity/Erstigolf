using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertSystem : MonoBehaviour
{
    [SerializeField] GameObject alertBanner;
    [SerializeField] AudioSource sound;
    [SerializeField] TMPro.TextMeshProUGUI displayText;
    [SerializeField] float displayTime = 3f;
    private static AlertSystem instance;

    private Queue<string> msgs = new Queue<string>();

    private float timeRemain;

    private void Awake() {
        instance = this;
    }

    private void Update() {
        if (timeRemain <= 0f){
            if (msgs.TryDequeue(out string msg)){
                displayText.text = msg;
                timeRemain = displayTime;
                alertBanner.SetActive(true);
                sound.Play();
            }
            else {
                alertBanner.SetActive(false);
            }
        }
        else {
            timeRemain -= Time.deltaTime;
        }
    }

    public static void Message(string msg){
        instance.msgs.Enqueue(msg);
    }
}
