using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [Tooltip("Type of this ingredient")]
    public Ingredients type;

    [Tooltip("GameObject that will be activated before destroying this object")]
    public GameObject splash;

    [Tooltip("Time to wait until ingredient is destroyed")]
    public float splashDuration = 1.5f;

    private static readonly Dictionary<Ingredients, Func<Unicorn, bool>> rewards = new Dictionary<Ingredients, Func<Unicorn, bool>>()
    {
        { Ingredients.Olive, unicorn => unicorn.abilityHolder.SetToothpickAbility() },
        { Ingredients.Peperoni, unicorn => unicorn.abilityHolder.SetNimbusAbility() }
    };

    public void TimeUp(HexagonCell hexagonCell)
    {
        // Give player the reward specified by the ingredient
        if (hexagonCell.Player.HasValue)
        {
            if (!rewards.ContainsKey(type)) { Debug.LogWarning("Ingredient '" + type + "' does not have a reward defined"); }
            else
            {
                var unicorn = FindObjectsOfType<Unicorn>().FirstOrDefault(u => u.player == hexagonCell.Player.Value);
                if (unicorn != null && !rewards[type](unicorn)) { Debug.LogWarning("Reward could not be provided successfully for '" + type + "'"); }
            }
        }

        // Destroy ingredient
        if (splash == null)
        {
            Destroy(gameObject);
        }
        else
        {
            // Deactivate colliders (the ingredient is no longer technically there)
            foreach (var collider in GetComponents<Collider2D>()) { collider.enabled = false; }
            // Only let the splash effect active
            for(int i = 0; i < transform.childCount; i++) { transform.GetChild(i).gameObject.SetActive(false); }
            splash.SetActive(true);
            // Destroy after specified amount of time
            Destroy(gameObject, splashDuration);
        }
    }

    /// <summary>Activates / Deactivates all 2D colliders that are attached to this GameObject or any of its children.</summary>
    public void SetCollidersActive(bool active)
    {
        foreach (var collider in GetComponentsInChildren<Collider2D>())
        {
            collider.enabled = active;
        }
    }

    /// <summary>Deactivate colliders after waiting a given amount of seconds (or immediately if 0 is given).</summary>
    /// <param name="time">Amount of seconds to wait before activating colliders.</param>
    public void WaitThenActivateColliders(float time)
    {
        if (time < Mathf.Epsilon) { SetCollidersActive(true); }
        else { Invoke("ActivateColliders", time); }
    }

    private void ActivateColliders() { SetCollidersActive(true); }

}
