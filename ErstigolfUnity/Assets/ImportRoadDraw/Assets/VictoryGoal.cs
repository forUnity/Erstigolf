using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VictoryGoal : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == playerTag)
        {
            GameManager.instance.Win();
            gameObject.SetActive(false);
        }
    }
}
