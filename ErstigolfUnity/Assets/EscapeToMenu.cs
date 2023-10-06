using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeToMenu : MonoBehaviour
{
    public void ReturnToMenu()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            Application.Quit();
        } else
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ReturnToMenu();
    }
}
