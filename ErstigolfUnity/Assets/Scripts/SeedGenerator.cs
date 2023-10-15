using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeedGenerator : MonoBehaviour
{
    [SerializeField] TMP_InputField field;
    public void Generate(){
        string s = Random.Range(0, int.MaxValue).ToString();
        field.text = s;
        field.onEndEdit.Invoke(s);
    }
}
