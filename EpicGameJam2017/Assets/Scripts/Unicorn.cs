﻿using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Unicorn : MonoBehaviour
{
    [Tooltip("Player which controls this unicorn")]
    public Players player;

    [Tooltip("Wheter the unicorn is currently switched on or not (can be changed by the player with the Switch key)")]
    public bool controlsActive = false;

    [Tooltip("Unicorn turnspeed in degrees per second")]
    public float turnSpeed = 1f;

    /// <summary>Don't change this value at runtime. <see cref="SpeedForce"/> should be used for that.</summary>
    [Tooltip("Force which is added when the action key is pressed")]
    public float speedForce = 10f;

    [Tooltip("How much the unicorn dirfts")]
    public float driftFactor = 0.25f;

    [Tooltip("Marks the tip of the green horn")]
    public Transform marker;

    [Tooltip("Box that stores the unicorn ability until it is used")]
    public AbilityHolder abilityHolder;

    [Tooltip("Prefab of the toothpick which is used in the toothpick ability")]
    public Toothpick toothpickPrefab;

    private Ingredient ingredient = null;
    private Controller controller = null;

    /// <summary>Ingredient, which this unicorn currently carries.</summary>
    public Ingredient CarryIngredient { get { return ingredient; } }

    /// <summary>
    /// Stores the difference between unicorn and ability holder position,
    /// so the position of the ability holder relative to the unicorn can later be restored
    /// </summary>
    private Vector3 abilityHolderPositionDifference;

    /// <summary>Set the given ingredient to belong to this unicorn.</summary>
    public bool SetIngredient(Ingredient ingredient)
    {
        if(this.ingredient != null) { return false; }
        this.ingredient = ingredient;
        ingredient.SetCollidersActive(false);
        return true;
    }

    public float SpeedForce { get; set; }

    public void Awake()
    {
        SpeedForce = speedForce;
        controller = FindObjectOfType<Controller>();
        if(controller == null) { throw new System.ArgumentException(); }
        abilityHolderPositionDifference = abilityHolder.transform.position - transform.position;
    }

    public void Update()
    {
        if (controlsActive && Input.GetButtonDown(Constants.SpecialButton + player))
        {
            // Place ingredient
            if(ingredient != null)
            {
                if (controller.DropIngredientOnPizza(ingredient))
                {
                    ingredient.WaitThenActivateColliders(1f);
                    ingredient = null;
                }
            }
            // Activate powerup (only works if slot is not empty)
            else
            {
                abilityHolder.CastAbility(this);
            }
        }
        // Switch between unicorn and train
        if(Input.GetButtonDown(Constants.SwitchButton + player))
        {
            controlsActive = !controlsActive;
        }
    }

    public void FixedUpdate()
    {
        if (controlsActive)
        {
            var body = GetComponent<Rigidbody2D>();

            body.velocity = ForwardVelocity() + driftFactor * RightVelocity();

            // Turn unicorn
            body.angularVelocity = Input.GetAxis(Constants.HorizontalAxis + player) * turnSpeed;
            // Accelerate unicorn
            if (Input.GetButton(Constants.ActionButton + player))
            {
                body.AddForce(transform.up * SpeedForce, ForceMode2D.Force);
            }
        }
    }

    private void LateUpdate()
    {
        if(ingredient != null)
        {
            ingredient.transform.position = marker.position;
        }
        abilityHolder.transform.position = transform.position + abilityHolderPositionDifference;
        abilityHolder.transform.rotation = Quaternion.identity;
    }

    /// <summary>Forward part of the velocity</summary>
    private Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    /// <summary>Forward part of the velocity</summary>
    private Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    /// <summary>
    /// Instantiates a toothpick and throws it in the direction that this unicorn is facing.
    /// </summary>
    public void ThrowToothpick()
    {
        var toothpick = Instantiate(toothpickPrefab, transform.position, transform.rotation);
        toothpick.GetComponent<Rigidbody2D>().velocity = transform.up;
    }
}
