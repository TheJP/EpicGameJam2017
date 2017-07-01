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

    [Tooltip("Force which is added when the action key is pressed")]
    public float speedForce = 10f;

    [Tooltip("How much the unicorn dirfts")]
    public float driftFactor = 0.25f;

    public void Update()
    {
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
                body.AddForce(transform.up * speedForce, ForceMode2D.Force);
            }
        }
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
}
