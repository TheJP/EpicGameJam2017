using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRegistration : MonoBehaviour {

    public MenuController menuController;
    private ScrollRect scrollRect;
    private RectTransform scrollContent;

    [Tooltip("Canvas to be shown when a player joins")]
    public Canvas playerCanvasPrefab;

 
    void Awake()
    {
        if (!menuController)
        {
            Debug.LogWarning("no MenuController attached. Attempting to Find() it");
            menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        }
        scrollRect = GetComponent<ScrollRect>();
        if (!scrollRect) Debug.LogWarning("no scroll rect found!!");

        scrollContent = scrollRect.transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        if(!scrollContent) print("no scrollcontent found!");
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
                    
                    //Listeneintrag erstellen hackky hack hack

                    var playercanvas = Instantiate(playerCanvasPrefab, scrollContent);
                    var inputfield = playercanvas.GetComponentInChildren<InputField>();
                    inputfield.text = nextPlayerLetter;
                    // inputfield.onValidateInput += //TODO do something 
                    var rect = playercanvas.GetComponent<RectTransform>();
                    rect.localPosition = new Vector3(100,-15 - 30*(registeredPlayers.Count-1),0);

                }
            }


        }



        int nextPlayer = menuController.getHighestPlayer();
        Players test = (Players)nextPlayer;
        


        
    }
}
