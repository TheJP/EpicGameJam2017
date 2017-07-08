using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientCountdown : MonoBehaviour
{
    private float startTime;
    private Ingredient ingredient;
    private HexagonCell hexagonCell;
    public int countdownStart = 5;
    public int Countdown
    {
        get { return countdownStart - (int)(Time.time - startTime); }
    }
    private Vector3 textScale;

    public Text text;

    public void RefreshCountdown(int countdown, Ingredient ingredient, HexagonCell hexagonCell)
    {
        countdownStart = GlobalData.VegiCountdown;
        this.ingredient = ingredient;
        this.hexagonCell = hexagonCell;
        startTime = Time.time;
    }

    public void Start()
    {
        textScale = text.transform.localScale;
    }

    public void Update()
    {
        // Flashy animation
        var timeDifference = Time.time - startTime;
        var subDecimal = timeDifference - (int)timeDifference;
        text.transform.localScale = textScale + textScale * -(subDecimal - 0.5f);

        // Update text and remove after countdown finished
        if (Countdown <= 0)
        {
            if (ingredient != null) { ingredient.TimeUp(hexagonCell); }
            Destroy(gameObject);
            return;
        }
        text.text = Countdown.ToString();
    }
}
