using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleRotation
  : MonoBehaviour
{
  public Players player;
  
  public float rotationArc = 90.0f;
  public float rotationSpeed = 60.0f;

  private float currentRotation;
  private float maxRotationLeft;
  private float maxRotationRight;

	// Use this for initialization
	void Start ()
	{
	  currentRotation = 0;

	  maxRotationLeft = rotationArc / 2;
	  maxRotationRight = -rotationArc / 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
	  if(Input.GetButton(Constants.LeftButton + player))
	  {
	    var rotation = Time.deltaTime * rotationSpeed;
	    currentRotation += rotation;

	    if(currentRotation > maxRotationLeft)
	    {
	      rotation -= currentRotation - maxRotationLeft;
	      currentRotation = maxRotationLeft;
	    }

	    transform.RotateAround(Vector3.zero, Vector3.forward, rotation);
    }
    else if(Input.GetButton(Constants.RightButton + player))
	  {
	    var rotation = Time.deltaTime * -rotationSpeed;
	    currentRotation += rotation;
      
	    if(currentRotation < maxRotationRight)
	    {
	      rotation -= currentRotation - maxRotationRight;
	      currentRotation = maxRotationRight;
	    }

	    transform.RotateAround(Vector3.zero, Vector3.forward, rotation);
    }
	}
}
