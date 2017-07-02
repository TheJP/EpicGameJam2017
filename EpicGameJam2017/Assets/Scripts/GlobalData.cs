using System;

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;


public static class GlobalData
{
    private static System.Collections.Generic.List<Players> players = new System.Collections.Generic.List<Players>();

    public static System.Collections.Generic.List<Players> Players
    {
        get
        {
            return players;
        }
        set
        {
            players = value;
        }
    }

    private static readonly Dictionary<Players, int> playerScores = new Dictionary<Players, int>();
    private static Text playerScoreView;

    public static void SetPlayerScoreView(Text view)
    {
        playerScoreView = view;
        UpdatePlayerScoreView();
    }

    private static void UpdatePlayerScoreView()
    {
        if(playerScoreView == null)
        {
            return;
        }

        var text = "";
        foreach(var playerScore in playerScores)
        {
            if(text.Length > 0)
            {
                text += "\r\n";
            }

            var playerColor = Constants.PlayerColors[playerScore.Key];
            text += String.Format(
                "<color=#{2:x2}{3:x2}{4:x2}ff>Player {0}: {1}</color>",
                playerScore.Key,
                playerScore.Value,
                (int)(playerColor.r * 255),
                (int)(playerColor.g * 255),
                (int)(playerColor.b * 255));
        }
        playerScoreView.text = text;
    }

    public static int GetScore(Players player)
    {
        int score;
        if(playerScores.TryGetValue(player, out score))
        {
            return score;
        }

        return 0;
    }

    public static void AddToScore(Players player, int score)
    {
        if(playerScores.ContainsKey(player))
        {
            playerScores[player] += score;
        }
        else
        {
            playerScores.Add(player, score);
        }

        UpdatePlayerScoreView();
    }

    public static void ClearScores()
    {
        playerScores.Clear();
        UpdatePlayerScoreView();
    }
}