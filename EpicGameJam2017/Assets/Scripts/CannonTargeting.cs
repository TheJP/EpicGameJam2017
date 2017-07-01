using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTargeting : MonoBehaviour
{
  private bool isFiring;
  private float firingDistance;
  private Cannon cannon;

	// Use this for initialization
	void Start()
	{
	  cannon = GetComponentInParent<Cannon>();
	}
	
	// Update is called once per frame
	void Update ()
  {
    if(Input.GetButton(Constants.ActionButton + cannon.player))
    {
      // The button is being pressed, increase the distance we will fire
      isFiring = true;
      var distance = cannon.fireDistanceSpeed * Time.deltaTime;

      firingDistance += distance;
      if(firingDistance > cannon.maxFireDistance)
      {
        distance -= firingDistance - cannon.maxFireDistance;
        firingDistance = cannon.maxFireDistance;
      }

      transform.position += transform.up * distance;
    }
    else if(isFiring)
    {
      var target = transform.position;

      // The button was raised and we are currently in firing modus and thus should fire
      isFiring = false;
      transform.position -= transform.up * firingDistance;
      firingDistance = 0;

      StartCoroutine(FireShell(target));
    }
	}

  IEnumerator FireShell(Vector3 target)
  {
    var shell = Instantiate(cannon.shell, transform.position, transform.rotation);
    yield return null;

    while(!shell.transform.position.Equals(target))
    {
      shell.transform.position = Vector3.MoveTowards(shell.transform.position, target, Time.deltaTime * 10f);
      yield return null;
    }
    
    // BOOM!
    Destroy(shell);
  }
}
