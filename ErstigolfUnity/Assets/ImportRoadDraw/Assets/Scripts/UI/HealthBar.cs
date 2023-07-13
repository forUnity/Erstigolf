using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance;

    [SerializeField] private RectTransform remainingHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SetRemainingHealth(1f);
    }

    public void SetRemainingHealth(float percentage)
    {
        remainingHealth.anchorMax = new Vector2(percentage, remainingHealth.anchorMax.y);
    }
}
