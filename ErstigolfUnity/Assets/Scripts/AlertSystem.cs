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
    [SerializeField] int maxMsgCount = 3;
    private static AlertSystem instance;
    [SerializeField] private AlertSystem forwardToAlertSystem;
    public bool shouldBeInstance = true;
    private Queue<string> msgs = new Queue<string>();

    private float timeRemain;

    private void Awake() {
        if(shouldBeInstance && instance == null)
        {
            instance = this;
        }
    }

    private void Update() {
        if (timeRemain <= 0f){
            if (msgs.TryDequeue(out string msg)){
                if(forwardToAlertSystem)
                    forwardToAlertSystem.msgs.Enqueue(msg);

                displayText.text = msg;
                timeRemain = displayTime;
                alertBanner.SetActive(true);
                if (shouldBeInstance)
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
        while (instance.msgs.Count >= instance.maxMsgCount)
            instance.msgs.Dequeue();
        instance.msgs.Enqueue(msg);
    }
}
