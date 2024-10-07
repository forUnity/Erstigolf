using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

public class Leaderboard : MonoBehaviour
{
    public string URL =  "https://leaderboard.chicken-zebra.ts.net/submit";

    [ContextMenu("Test")]
    public void Test()//succes
    {
        SubmitScore(0, "Test");
    }

    [SerializeField] private TMPro.TMP_InputField nameInput;

    public void Submit()
    {
        SubmitScore(GetComponent<ScoreMenuNav>().ScoreCache, nameInput.text);
    }

    public async void SubmitScore(int score, string teamName)
    {
        var json = $"{{\"teamname\":\"{(teamName.Replace("\"", "&quot;").Replace("'", "&apos;").Replace("\\", ""))}\", \"score\":{score}}}";

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        var response = await client.PostAsync(URL, data);
    }


}
