using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CannonWaggon : MonoBehaviour
{
    [Tooltip("The player controlling this waggon")]
    public Players player;

    [Tooltip("The rotation arc the turrets are able to turn (left & right)")]
    public float rotationArc = 90.0f;

    [Tooltip("The speed at which the turrets turn")]
    public float rotationSpeed = 60.0f;

    [Tooltip("The maximum distance the turrets are able to fire")]
    public float maxFireDistance = 15;

    [Tooltip("How fast it is possible to fire longer distances")]
    public float fireDistanceSpeed = 10;

    [Tooltip("The tomato targeting game object")]
    public CannonTargeting tomatoCannon;

    [Tooltip("The cheese targeting game object")]
    public CannonTargeting cheeseCannon;

    private bool isTomatoFiring;
    private bool isUsedByPlayer;

    private MuzzleRotation[] muzzlesRotations;

    // Use this for initialization
    void Start()
    {
        tomatoCannon.EnableFiring();
        cheeseCannon.DisableFiring();

        muzzlesRotations = GetComponentsInChildren<MuzzleRotation>();

        isTomatoFiring = true;
        isUsedByPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(Constants.SwitchButton + player))
        {
            if(isUsedByPlayer)
            {
                tomatoCannon.DisableFiring();
                cheeseCannon.DisableFiring();

                foreach(var muzzlesRotation in muzzlesRotations)
                {
                    muzzlesRotation.SetEnabled(false);
                }
            }
            else
            {
                foreach(var muzzlesRotation in muzzlesRotations)
                {
                    muzzlesRotation.SetEnabled(true);
                }

                SwitchFiringCannons();
            }

            isUsedByPlayer = !isUsedByPlayer;
        }

        if(isUsedByPlayer && Input.GetButtonDown(Constants.SpecialButton + player))
        {
            SwitchFiringCannons();

            isTomatoFiring = !isTomatoFiring;
        }
    }

    private void SwitchFiringCannons()
    {
        if(isTomatoFiring)
        {
            tomatoCannon.DisableFiring();
            cheeseCannon.EnableFiring();
        }
        else
        {
            tomatoCannon.EnableFiring();
            cheeseCannon.DisableFiring();
        }
    }
}
