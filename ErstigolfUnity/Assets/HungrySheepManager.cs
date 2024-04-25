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
    [SerializeField] private float sheepSpawnProbability = 0.4f;
    public void MissedPizzaDelivery(Vector3 atPosition) {
        if(sheepSpawnProbability < Random.value) return;
        GetComponent<GhostSheepWarningUI>()?.ShowWarning();
        Instantiate(sheepPrefab, atPosition, Quaternion.identity).GetComponent<DamageDetector>().playerTarget = playerTarget;
    }
}
