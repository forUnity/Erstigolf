using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardApiKeyField : MonoBehaviour
{
    void Start() {
        var inputField = GetComponent<TMPro.TMP_InputField>();
        inputField.text = PlayerPrefs.GetString(Leaderboard.PlayerPrefAPIKey);
        inputField.onEndEdit.AddListener((string apiKey) => SaveApiKey(apiKey));
    }
    public void SaveApiKey(string apiKey)
    {
        PlayerPrefs.SetString(Leaderboard.PlayerPrefAPIKey, apiKey);
    }
}
