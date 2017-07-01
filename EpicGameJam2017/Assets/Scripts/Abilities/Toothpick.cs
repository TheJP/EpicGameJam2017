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
        var ingredient = collision.gameObject.GetComponent<Ingredient>() != null;
        var provider = collision.gameObject.GetComponent<IngredientProvider>() != null;
        if (ingredient || provider)
        {
            // Collision with other ingredients or with the bowl kills the toothpick
            Destroy(gameObject);
        }
        if (collision.gameObject == Thrower.gameObject) { return; }
        var unicorn = collision.gameObject.GetComponent<Unicorn>();
        if(unicorn != null)
        {
            unicorn.Stun();
            Destroy(gameObject);

        }
    }
}
