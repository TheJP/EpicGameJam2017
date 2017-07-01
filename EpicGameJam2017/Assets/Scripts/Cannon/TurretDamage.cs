using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDamage : MonoBehaviour
{
  private CannonTargeting cannonTargeting;

	// Use this for initialization
	void Start ()
	{
	  cannonTargeting = GetComponentInChildren<CannonTargeting>();
	}
	
  void OnTriggerEnter2D(Collider2D other)
  {
    cannonTargeting.Break();

    Destroy(other.gameObject);
  }
}
