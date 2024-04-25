using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSheepWarningUI : MonoBehaviour
{
    public float warningDuration = 1f;
    public GameObject warningUI1;
    public GameObject warningUI2;
    void Start()
    {
        toggleWarningText(false);
    }
    public void ShowWarning() {
        StartCoroutine(showWarningCoroutine());
    }
    public void toggleWarningText(bool show)
    {
        warningUI1.SetActive(show);
        warningUI2.SetActive(show);
    }
    private IEnumerator showWarningCoroutine() {
        toggleWarningText(true);
        yield return new WaitForSeconds(warningDuration);
        toggleWarningText(false);
    }
}
