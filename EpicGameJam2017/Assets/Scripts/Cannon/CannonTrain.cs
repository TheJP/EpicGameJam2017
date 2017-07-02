using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTrain : MonoBehaviour
{
  public Vector3 rotationCenter = Vector3.zero;
  public float distanceFromCenter = 5;
  public float rotationSpeed = 60.0f;

	// Use this for initialization
	void Start ()
	{
	  var toCenter = transform.position - rotationCenter;
	  var currentDistance = toCenter.magnitude;
	  float scale = distanceFromCenter / currentDistance;
    
	  transform.Translate(toCenter * scale, Space.World);

	  LookAt2D(rotationCenter);
	}
	
	// Update is called once per frame
	void Update ()
	{
	  transform.RotateAround(rotationCenter, transform.forward, Time.deltaTime * rotationSpeed);
	}

  private void LookAt2D(Vector3 point)
  {
    var dx = point.x - transform.position.x;
    var dy = point.y - transform.position.y;
    var angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0, 0, angle - 90);
  }
}
