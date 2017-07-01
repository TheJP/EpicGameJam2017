using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class IngredientProvider : MonoBehaviour
{
    [Tooltip("Prefabs of ingredients that this provider can give to unicorns")]
    public Ingredient[] ingredientPrefabs;

    [Tooltip("Transform that will be parent (group of) all ingredients")]
    public Transform ingredients;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var unicorn = collision.GetComponent<Unicorn>();
        if (unicorn != null && unicorn.CarryIngredient == null)
        {
            var ingredient = Instantiate(ingredientPrefabs[Random.Range(0, ingredientPrefabs.Length)], ingredients);
            if (!unicorn.SetIngredient(ingredient))
            {
                Destroy(ingredient);
            }
        }
    }
}
