using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

public class CannonTargeting : MonoBehaviour
{
    [Tooltip("The shell that will be copied and fired")]
    public GameObject shell;

    private bool isBroken;
    private bool isFiringAllowed;
    private bool isFiring;
    private float firingDistance;
    private CannonWaggon cannon;
    private SpriteRenderer crossHairRenderer;

    /// <summary>Indicates if the player marker should mark this turret.</summary>
    public bool ShouldBeMarked { get { return isFiringAllowed; } }

    // Use this for initialization
    void Start()
    {
        cannon = GetComponentInParent<CannonWaggon>();
        crossHairRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        crossHairRenderer.enabled = false;

        if (isFiringAllowed && !isBroken && Input.GetButton(Constants.ActionButton + cannon.player))
        {

            // The button is being pressed, increase the distance we will fire
            isFiring = true;
            crossHairRenderer.enabled = true;
            var distance = cannon.fireDistanceSpeed * Time.deltaTime;

            firingDistance += distance;
            if (firingDistance > cannon.maxFireDistance)
            {
                distance -= firingDistance - cannon.maxFireDistance;
                firingDistance = cannon.maxFireDistance;
            }

            transform.position += -transform.right * distance;
        }
        else if (isFiring)
        {
            var target = transform.position;

            // The button was raised and we are currently in firing modus and thus should fire
            isFiring = false;
            transform.position -= -transform.right * firingDistance;
            firingDistance = 0;

            var shellBody = Instantiate(this.shell, transform.position, transform.rotation);
            var shell = shellBody.GetComponent<Shell>();
            shell.Player = cannon.player;
            shell.Goto(target, 10.0f);
        }
    }

    public void EnableFiring()
    {
        isFiringAllowed = true;
    }

    public void DisableFiring()
    {
        isFiringAllowed = false;
        isFiring = false;
    }

    public void Break()
    {
        isBroken = true;
        isFiring = false;

        transform.position -= -transform.right * firingDistance;
        firingDistance = 0;
    }

    public void Repair()
    {
        isBroken = false;
    }
}
