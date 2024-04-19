using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungrySheepManager : MonoBehaviour
{
    public Transform playerTarget;
    public static HungrySheepManager instance;
    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    [SerializeField] private GameObject sheepPrefab;
    public void MissedPizzaDelivery(Vector3 atPosition) {
        Instantiate(sheepPrefab, atPosition, Quaternion.identity).GetComponent<DamageDetector>().playerTarget = playerTarget;
    }
}
