using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreMenuNav : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start() {
        scoreText.text = PizzaDeliveryManager.instance.score.ToString();
    }

    public void Back(){
        SceneManager.LoadSceneAsync(0);
    }
}
