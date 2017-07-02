using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Toothpick : MonoBehaviour
{
    [Tooltip("Speed in game units per second")]
    public float speed;

    public Unicorn Thrower { get; set; }

    public void Update()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collision with other ingredients or with the bowl kills the toothpick
        var ingredient = collision.gameObject.GetComponent<Ingredient>() != null;
        var provider = collision.gameObject.GetComponent<IngredientProvider>() != null;
        if (ingredient || provider)
        {
            Destroy(gameObject);
        }

        // Don't damage the unicorn that casted the toothpick
        if (collision.gameObject == Thrower.gameObject) { return; }

        // Stun unicorns if hit
        var unicorn = collision.gameObject.GetComponent<Unicorn>();
        if (unicorn != null)
        {
            unicorn.Stun();
            Destroy(gameObject);

        }

        // Damage turret if hit
        var turret = collision.gameObject.GetComponent<TurretDamage>();
        if (turret != null)
        {
            turret.Damage();
            Destroy(gameObject);
        }

        // Remove toothpick if border was reached
        if (collision.gameObject.tag == "WorldBorder")
        {
            Destroy(gameObject);
        }
    }
}
