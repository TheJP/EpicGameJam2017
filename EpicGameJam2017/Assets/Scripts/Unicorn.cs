using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Unicorn : MonoBehaviour
{
    [Tooltip("Player which controls this unicorn")]
    public Players player;

    [Tooltip("Wheter the unicorn is currently switched on or not (can be changed by the player with the Switch key)")]
    public bool controlsActive = false;

    [Tooltip("Unicorn turnspeed (torque force modifier)")]
    public float turnSpeed = 1f;

    [Tooltip("Force which is added when the action key is pressed")]
    public float speedForce = 10f;

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
            // Turn unicorn
            body.AddTorque(Input.GetAxis(Constants.HorizontalAxis + player) * turnSpeed, ForceMode2D.Force);
            // Accelerate unicorn
            if (Input.GetButton(Constants.ActionButton + player))
            {
                body.AddForce(transform.up * speedForce, ForceMode2D.Force);
            }
        }
    }
}
