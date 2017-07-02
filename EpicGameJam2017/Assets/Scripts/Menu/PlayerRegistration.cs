using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRegistration : MonoBehaviour
{

    public MenuController menuController;
    private ScrollRect scrollRect;
    private RectTransform scrollContent;

    [Tooltip("Canvas to be shown when a player joins")]
    public Canvas playerCanvasPrefab;

    [Tooltip("Font used for list entries")]
    public Font listFont;


    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        if (!scrollRect) Debug.LogWarning("no scroll rect found!!");

        scrollContent = scrollRect.transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        if (!scrollContent) print("no scrollcontent found!");
    }

    private List<Players> registeredPlayers = new List<Players>();

    // Update is called once per frame
    void Update()
    {
        var values = Enum.GetValues(typeof(Players));
        foreach (Players player in values)
        {
            string nextPlayerLetter = Enum.GetName(typeof(Players), player);
            string nextButtonName = Constants.ActionButton + nextPlayerLetter;
            // Innocent viewer: What?
            if (Input.GetButtonDown(nextButtonName)
                || Input.GetButtonDown(nextButtonName)
                || Input.GetButtonDown(nextButtonName))
            {
                if (!registeredPlayers.Contains(player))
                {
                    registeredPlayers.Add(player);
                    menuController.AddPlayer(player);

                    //Listeneintrag erstellen hackky hack hack

                    var playercanvas = Instantiate(playerCanvasPrefab, scrollContent);
                    var inputfield = playercanvas.GetComponentInChildren<InputField>();
                    inputfield.text = nextPlayerLetter;
                    inputfield.readOnly = true;
                    // Customize font of list entry
                    foreach (var text in playercanvas.GetComponentsInChildren<Text>()) { text.font = listFont; }
                    foreach (var text in inputfield.GetComponentsInChildren<Text>()) { text.resizeTextForBestFit = true; }
                    // inputfield.onValidateInput += //TODO do something 
                    var rect = playercanvas.GetComponent<RectTransform>();
                    rect.localPosition = new Vector3(100, -15 - 30 * (registeredPlayers.Count - 1), 0);

                }
            }
        }
    }
}
