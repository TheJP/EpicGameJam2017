using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Tooltip("Play field that is managed by this controller")]
    public HexagonGrid hexagonGrid;

    [Tooltip("Amount of seconds a ingredient takes to respawn after a refresh")]
    public int ingredientCountdownTime = 5;

    [Tooltip("Prefab of the ingredient countdown")]
    public IngredientCountdown countdownPrefab;

    public bool DropIngredientOnPizza(Ingredient ingredient)
    {
        // Check for nearby hexagon tiles
        var minimalDistance = float.PositiveInfinity;
        Transform closest = null;
        for (int i = hexagonGrid.transform.childCount - 1; i >= 0; --i)
        {
            var child = hexagonGrid.transform.GetChild(i);
            var distance = Vector2.SqrMagnitude(child.position - ingredient.transform.position);
            if (distance < minimalDistance)
            {
                minimalDistance = distance;
                closest = child;
            }
        }

        // Does a closest hexagon tile exist?
        if (closest != null && Mathf.Sqrt(minimalDistance) < 2 * hexagonGrid.HexCellOuterRadius)
        {
            // Set ingredient position to the middle of the hexcell
            ingredient.transform.position = new Vector3(closest.position.x, closest.position.y, ingredient.transform.position.z);

            // Setup countdown for ingredient
            var countdown = Instantiate(countdownPrefab, ingredient.transform);
            countdown.RefreshCountdown(ingredientCountdownTime, ingredient);
            return true;
        }
        return false;
    }
}
