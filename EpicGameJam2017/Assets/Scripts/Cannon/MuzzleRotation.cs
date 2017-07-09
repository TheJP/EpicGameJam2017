using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleRotation : MonoBehaviour, PlayerMarkable
{
    private CannonWaggon cannon;
    private CannonTargeting targeting;

    private bool isEnabled;
    private float currentRotation;
    private float maxRotationLeft;
    private float maxRotationRight;

    public bool ShouldBeMarked { get { return targeting.ShouldBeMarked; } }

    public Players Player { get { return GetComponentInParent<CannonWaggon>().player; } }

    public Vector3 Position { get { return transform.position; } }

    // Use this for initialization
    void Start()
    {
        isEnabled = true;
        cannon = GetComponentInParent<CannonWaggon>();
        targeting = GetComponentInChildren<CannonTargeting>();

        currentRotation = 0;

        maxRotationLeft = cannon.rotationArc / 2;
        maxRotationRight = -cannon.rotationArc / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled)
        {
            return;
        }

        var direction = Input.GetAxis(Constants.HorizontalAxis + cannon.player);

        var rotation = Time.deltaTime * cannon.rotationSpeed * direction;
        currentRotation += rotation;

        if (currentRotation < maxRotationRight)
        {
            rotation -= currentRotation - maxRotationRight;
            currentRotation = maxRotationRight;
        }

        if (currentRotation > maxRotationLeft)
        {
            rotation -= currentRotation - maxRotationLeft;
            currentRotation = maxRotationLeft;
        }

        transform.Rotate(transform.forward, rotation);
    }

    public void SetEnabled(bool enabled)
    {
        isEnabled = enabled;
    }
}
