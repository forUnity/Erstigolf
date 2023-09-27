using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaWaitingPersonGfx : MonoBehaviour
{
    [SerializeField] private List<GameObject> gfxs;
    private void Start()
    {
        int activeGfx = Random.Range(0, gfxs.Count);
        for (int i = 0; i < gfxs.Count; i++)
        {
            gfxs[i].SetActive(i == activeGfx);
        }
    }
}
