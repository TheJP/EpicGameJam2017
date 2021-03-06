﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private List<Players> players = new List<Players>();

    public void AddPlayer(Players player)
    {
        if(players != null)
        {
            players.Add(player);
        }
    }

    public List<Players> getPlayers()
    {
        return this.players;
    }

    public int getHighestPlayer()
    {
        int highestPlayer = 0;
        foreach(Players player in players)
        {

            if((int) player > highestPlayer)
            {
                highestPlayer++;
            }
        }
        return highestPlayer;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            StartNewGame();
        }
    }

    public void StartNewGame()
    {
        if(players.Count == 0)
        {
            Debug.Log("No Players registered.");
            //TODO: Inform User....
        } else
        {
            //Give the List with Players to the MainScene to and start the Game.
            GlobalData.Players = this.players;
            SceneManager.LoadScene("MainScene");
            //PlayerPrefs
        }
    }
}
