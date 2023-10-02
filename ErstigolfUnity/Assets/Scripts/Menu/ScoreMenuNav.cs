using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreMenuNav : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    public int ScoreCache;

    private void Start() {
        scoreText.text = PizzaDeliveryManager.instance.score.ToString()+ "Hc";
        ScoreCache = PizzaDeliveryManager.instance.score;
    }

    public void Back(){
        SceneManager.LoadSceneAsync(0);
    }
}
