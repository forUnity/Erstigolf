using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    private ButtonsInput inputs;
    public delegate void PauseToggleEvent(bool paused);
    public static PauseToggleEvent toggleEvent;

    private void Awake() {
        toggleEvent = x => {};
        ChangeMenu(false);
        inputs = new ButtonsInput();
        inputs.Menu.TogglePause.performed += x => ToggleMenu();
    }

    private void OnEnable() {
        inputs.Enable();
    }

    private void OnDisable() {
        toggleEvent = x => {};
        ChangeMenu(false);
        inputs.Disable();
    }
    
    public void ReturnToMainMenu(){
        SceneManager.LoadSceneAsync(0);
    }

    public void ToggleMenu(){
        ChangeMenu(!pausePanel.activeSelf);
    }

    private void ChangeMenu(bool paused){
        pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
        toggleEvent(paused);
        bool lockCursor = PlayerPrefs.HasKey("SoloMode") && PlayerPrefs.GetInt("SoloMode") == 1 && !paused;
        Cursor.lockState = lockCursor ?  CursorLockMode.Locked : CursorLockMode.None;
    }
}
