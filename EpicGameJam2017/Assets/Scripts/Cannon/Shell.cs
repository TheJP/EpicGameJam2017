using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
  private Vector3 target;
  private float speed;
  
	// Update is called once per frame
	void Update ()
  {
    if(!transform.position.Equals(target))
    {
      transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
      return;
    }

    // BOOM!
    Destroy(gameObject);
  }

  public void Goto(Vector3 target, float speed)
  {
    this.target = target;
    this.speed = speed;
  }
}
