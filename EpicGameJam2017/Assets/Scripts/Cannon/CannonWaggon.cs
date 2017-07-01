using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonWaggon : MonoBehaviour
{
  public Players player;
  
  public CannonTargeting tomatoCannon;
  public CannonTargeting cheeseCannon;

  private bool isTomatoFiring;

	// Use this for initialization
	void Start ()
	{
	  tomatoCannon.EnableFiring();
	  cheeseCannon.DisableFiring();

	  isTomatoFiring = true;
	}
	
	// Update is called once per frame
	void Update ()
  {
    if(Input.GetButtonDown(Constants.SpecialButton + player))
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

      isTomatoFiring = !isTomatoFiring;
    }
	}
}
