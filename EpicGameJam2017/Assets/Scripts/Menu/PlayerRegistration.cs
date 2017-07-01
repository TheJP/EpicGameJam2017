using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegistration : MonoBehaviour {

    public MenuController menuController;

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
