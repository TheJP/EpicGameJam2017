﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour {

    public MenuController menuController;
    public UserInformations userInformations;


    public void startGame()
    {
        int numberOfPlayers = menuController.getPlayers().Count;
        if ( numberOfPlayers == 0)
        {
            userInformations.Alert("Register Player First!", 4L);
        } else
        {
            menuController.StartNewGame();
        }
    }

    public void quitOnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
	
}
