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
	  var direction = Input.GetAxis(Constants.HorizontalAxis + cannon.player);
    
	  var rotation = Time.deltaTime * cannon.rotationSpeed * direction;
	  currentRotation += rotation;
      
	  if(currentRotation < maxRotationRight)
	  {
	    rotation -= currentRotation - maxRotationRight;
	    currentRotation = maxRotationRight;
	  }

	  if(currentRotation > maxRotationLeft)
	  {
	    rotation -= currentRotation - maxRotationLeft;
	    currentRotation = maxRotationLeft;
	  }

    transform.RotateAround(new Vector3(transform.position.x, transform.parent.position.y), transform.forward, rotation);
	}
}
