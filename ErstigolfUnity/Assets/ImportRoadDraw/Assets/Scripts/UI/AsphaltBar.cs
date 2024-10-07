using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsphaltBar : MonoBehaviour
{
    public static AsphaltBar Instance;

    [SerializeField] private RectTransform actuallyLeft;
    [SerializeField] private RectTransform indicatedLeft;

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
        SetActuallyLeft(1f);
    }

    public void SetActuallyLeft(float percentage)
    {
        actuallyLeft.anchorMax = new Vector2(percentage, actuallyLeft.anchorMax.y);
        ResetIndicatedLeft();
    }

    public void SetIndicatedLeft(float percentage)
    {
        indicatedLeft.anchorMax = new Vector2(percentage, indicatedLeft.anchorMax.y);
    }

    public void ResetIndicatedLeft()
    {
        indicatedLeft.anchorMax = actuallyLeft.anchorMax;
    }
}
