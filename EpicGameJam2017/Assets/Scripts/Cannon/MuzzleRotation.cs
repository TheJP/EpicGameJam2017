using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleRotation
  : MonoBehaviour
{
  private CannonWaggon cannon;

  private bool isEnabled;
  private float currentRotation;
  private float maxRotationLeft;
  private float maxRotationRight;

	// Use this for initialization
	void Start ()
	{
	  isEnabled = true;
	  cannon = GetComponentInParent<CannonWaggon>();

	  currentRotation = 0;

	  maxRotationLeft = cannon.rotationArc / 2;
	  maxRotationRight = -cannon.rotationArc / 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
	  if(!isEnabled)
	  {
	    return;
	  }

	  var direction = Input.GetAxis(Constants.HorizontalAxis + cannon.player);
    
	  var rotation = Time.deltaTime * cannon.rotationSpeed * -direction;
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

  public void SetEnabled(bool enabled)
  {
    isEnabled = enabled;
  }
}
