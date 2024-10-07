using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("ShowMinimap"))
        {
            gameObject.SetActive(PlayerPrefs.GetInt("ShowMinimap") == 1);
        }
    }
}
