using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;


public static class GlobalData
{
    private static System.Collections.Generic.List<Players> players = new System.Collections.Generic.List<Players>();

    public static List<Players> Players
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

    private static int pointsToWin = 20;
    public static int PointsToWin { get { return pointsToWin; } set { pointsToWin = value; } }

    private static int vegiCountdown = 10;
    public static int VegiCountdown { get { return vegiCountdown; } set { vegiCountdown = value; } }

    public static void SetPlayerScoreView(Text view)
    {
        playerScoreView = view;
        UpdatePlayerScoreView();
    }

    private static void UpdatePlayerScoreView()
    {
        int barLength = 10;
        if (playerScoreView == null)
        {
            return;
        }

        var text = "";
        foreach (var playerScore in playerScores)
        {
            if (text.Length > 0)
            {
                text += "\r\n";
            }

            var playerColor = Constants.PlayerColors[playerScore.Key];
            var playerScoreLength = (int)((playerScore.Value / (float)PointsToWin) * barLength);
            var playerBar = new string('▀', playerScoreLength);
            var bar = new string('▀', barLength - playerScoreLength);
            text += String.Format(
                "<color=#{2:x2}{3:x2}{4:x2}ff>Player {0}:{1:00} {5}</color>{6}",
                playerScore.Key,
                playerScore.Value,
                (int)(playerColor.r * 255),
                (int)(playerColor.g * 255),
                (int)(playerColor.b * 255),
                playerBar,
                bar);
        }
        playerScoreView.text = text;
    }

    public static int GetScore(Players player)
    {
        int score;
        if (playerScores.TryGetValue(player, out score))
        {
            return score;
        }

        return 0;
    }

    public static void AddToScore(Players player, int score)
    {
        if (playerScores.ContainsKey(player))
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