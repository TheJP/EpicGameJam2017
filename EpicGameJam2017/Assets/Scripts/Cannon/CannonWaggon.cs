using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonWaggon : MonoBehaviour
{
  public Players player;

  public Cannon tomatoCannon;
  public Cannon cheeseCannon;

	// Use this for initialization
	void Start ()
  {
	}
	
	// Update is called once per frame
	void Update ()
  {
    if(Input.GetButtonDown(Constants.SpecialButton + player))
    {
    }
	}
}
