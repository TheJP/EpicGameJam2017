using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegistration : MonoBehaviour {

    public MenuController menuController;

    void Awake()
    {
        if (!menuController)
        {
            Debug.LogWarning("no MenuController attached. Attempting to Find() it");
            menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        }
    }

    private List<Players> registeredPlayers = new List<Players>();
	
	// Update is called once per frame
	void Update () {

        var values = Enum.GetValues(typeof(Players));
        foreach(Players player in values)
        {
            string nextPlayerLetter = Enum.GetName(typeof(Players), player);
            string nextButtonName = Constants.ActionButton + nextPlayerLetter;
            if (Input.GetButtonDown(nextButtonName)
                || Input.GetButtonDown(nextButtonName)
                || Input.GetButtonDown(nextButtonName))
            {
                if (!registeredPlayers.Contains(player))
                {
                    Debug.Log("New Player " + nextPlayerLetter);
                    registeredPlayers.Add(player);
                    menuController.AddPlayer(player);
                    //Listeneintrag erstellen
                }
            }
        }



        int nextPlayer = menuController.getHighestPlayer();
        Players test = (Players)nextPlayer;
        


        
    }
}
