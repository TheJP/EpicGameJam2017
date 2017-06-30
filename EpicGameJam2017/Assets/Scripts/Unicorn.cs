using System.Collections.Generic;
using UnityEngine;

public class Unicorn : MonoBehaviour
{
    [Tooltip("Player which controls this unicorn")]
    public Players player;

    [Tooltip("Wheter the unicorn is currently switched on or not (can be changed by the player with the Switch key)")]
    public bool controlsActive = false;

    [Tooltip("Unicorn turnspeed in degrees per second")]
    public float turnSpeed = 360f;

    public void Update()
    {
        if (controlsActive)
        {
            // Turn unicorn
            if(Input.GetButton(Constants.LeftButton + player))
            {
                transform.Rotate(0f, 0f, turnSpeed * Time.deltaTime);
            }
            if(Input.GetButton(Constants.RightButton + player))
            {
                transform.Rotate(0f, 0f, -turnSpeed * Time.deltaTime);
            }
        }

        // Switch between unicorn and train
        if(Input.GetButtonDown(Constants.SwitchButton + player))
        {
            controlsActive = !controlsActive;
        }
    }
}
