using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageOnClickListener : MonoBehaviour
{

    public Sprite OffSprite;
    public Sprite OnSprite;

    private Image yourImage;

    public void Start()
    {
        yourImage = gameObject.GetComponent<Image>();
        yourImage.GetComponent<Image>().sprite = OnSprite;
    }

    public void Update()
    {
        yourImage.GetComponent<Image>().sprite = AudioListener.pause ? OffSprite : OnSprite;
    }

    public void toggleSound()
    {
        AudioListener.pause = !AudioListener.pause;
    }

}
