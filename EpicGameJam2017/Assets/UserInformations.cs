using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInformations : MonoBehaviour {

    public Sprite alertSprite;
    private Text alertText;
    private Image image;
    private float timer;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        alertText = gameObject.GetComponentInChildren<Text>();
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            ClearView();
        }
    }

    public void Alert(string text, float timeInSeconds)
    {
        image.sprite = alertSprite;
        Color color = image.color;
        color.a = 1;
        image.color = color;
        timer = timeInSeconds;

        alertText.text = text;
    }

    public void ClearView()
    {
        //image.sprite = null;
        Color color = image.color;
        color.a = 0;
        image.color = color;
        alertText.text = "";
    }
}
