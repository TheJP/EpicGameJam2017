using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


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
    }
}