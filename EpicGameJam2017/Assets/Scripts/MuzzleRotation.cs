using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleRotation
  : MonoBehaviour
{
  private Cannon cannon;

  private float currentRotation;
  private float maxRotationLeft;
  private float maxRotationRight;

	// Use this for initialization
	void Start ()
	{
	  cannon = GetComponentInParent<Cannon>();

	  currentRotation = 0;

	  maxRotationLeft = cannon.rotationArc / 2;
	  maxRotationRight = -cannon.rotationArc / 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
	  if(Input.GetButton(Constants.LeftButton + cannon.player))
	  {
	    var rotation = Time.deltaTime * cannon.rotationSpeed;
	    currentRotation += rotation;

	    if(currentRotation > maxRotationLeft)
	    {
	      rotation -= currentRotation - maxRotationLeft;
	      currentRotation = maxRotationLeft;
	    }

	    transform.RotateAround(Vector3.zero, transform.forward, rotation);
    }
    else if(Input.GetButton(Constants.RightButton + cannon.player))
	  {
	    var rotation = Time.deltaTime * -cannon.rotationSpeed;
	    currentRotation += rotation;
      
	    if(currentRotation < maxRotationRight)
	    {
	      rotation -= currentRotation - maxRotationRight;
	      currentRotation = maxRotationRight;
	    }

	    transform.RotateAround(Vector3.zero, transform.forward, rotation);
    }
	}
}
