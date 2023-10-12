using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class Onlylowercase : MonoBehaviour
{
    private TMP_InputField tm;
    private void Start()
    {
        tm = GetComponent<TMP_InputField>();
    }
    public void EnsureLower()
    {
        tm.text = tm.text.ToLower();
    }
}
