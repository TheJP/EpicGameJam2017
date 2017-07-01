using UnityEngine;
using UnityEditor;
using System.Collections;

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
}