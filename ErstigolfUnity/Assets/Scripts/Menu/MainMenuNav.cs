using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("quit");
    }

    static bool secScreenActive;
    private void Awake() {
        Cursor.lockState = CursorLockMode.None;
        if (!secScreenActive){
            secScreenActive = true;
            if( Display.displays.Length < 2)
            {
                Debug.LogError("Connect Second Display!!!");
            }
            else {
                Display.displays[1].Activate();
            }
        }
    }
}
