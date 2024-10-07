using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private float restartLevelAfterSeconds;

    [SerializeField] private GameObject restartTextHolder;
    [SerializeField] private GameObject winEffectHolder;
    [SerializeField] private GameObject looseEffectHolder;

    #region instance
    public static GameManager instance;
    private void Awake()
    {
        if(instance)
        {
            Debug.LogError("More than one GameManager ", this);
        }
        instance = this;
        looseEffectHolder.SetActive(false);
        winEffectHolder.SetActive(false);
    }
    #endregion
    private void Update()
    {
        if(canRestart)
        {
            if(Input.anyKeyDown)
            {
                RestartLevel();
            }
        }
    }
    public void Win()
    {
        winEffectHolder.SetActive(true);
        StartCoroutine(RestartLevelTimer());
    }

    public void Loose()
    {
        looseEffectHolder.SetActive(true);
        StartCoroutine(RestartLevelTimer());
    }

    private bool canRestart = false;
    private IEnumerator RestartLevelTimer()
    {
        yield return new WaitForSeconds(restartLevelAfterSeconds);
        restartTextHolder.SetActive(true);
        canRestart = true;
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
