using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) Play();
    }

    public void Play(){
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("quit");
    }

    bool secScreenActive;
    private void Awake() {
        if (!secScreenActive){
            secScreenActive = true;
            Display.displays[1].Activate();
        }
    }
}
