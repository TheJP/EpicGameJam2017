using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageOnClickListener : MonoBehaviour {

    public Sprite OffSprite;
    public Sprite OnSprite;

    private bool musicOn;
    private Image yourImage;

    void Start()
    {
        musicOn = true;
        yourImage = gameObject.GetComponent<Image>();
        yourImage.GetComponent<Image>().sprite = OnSprite;
    }

    public void toggleSound()
    {
        //TODO: Toggle sound
        if (musicOn)
        {
            musicOn = false;
            yourImage.GetComponent<Image>().sprite = OffSprite;
        }

        else
        {
            musicOn = true;
            yourImage.GetComponent<Image>().sprite = OnSprite;
        }
    }

}
