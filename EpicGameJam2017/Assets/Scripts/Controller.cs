using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Tooltip("Amount of seconds a ingredient takes to respawn after a refresh")]
    public int ingredientCountdownTime = 5;

    [Tooltip("Prefab of the ingredient countdown")]
    public IngredientCountdown countdownPrefab;

    public void DropIngredientOnPizza(Ingredient ingredient)
    {
        var countdown = Instantiate(countdownPrefab, ingredient.transform);
        countdown.RefreshCountdown(ingredientCountdownTime, ingredient);
    }
}
