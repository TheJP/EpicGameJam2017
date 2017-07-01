using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [Tooltip("Type of this ingredient")]
    public Indgredients type;

    public void TimeUp()
    {
        // TODO: Give player score and init animation
        Destroy(gameObject);
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
