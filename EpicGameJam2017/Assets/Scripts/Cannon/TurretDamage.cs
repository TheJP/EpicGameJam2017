﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDamage : MonoBehaviour
{
  [Tooltip("The time it takes to repair a turret")]
  public float repairTime = 5.0f;

  private float repairTimeLeft;
  private CannonTargeting cannonTargeting;
  private ParticleSystem damageParticles;

	// Use this for initialization
	void Start()
	{
	  cannonTargeting = GetComponentInChildren<CannonTargeting>();
	  damageParticles = GetComponentInChildren<ParticleSystem>();
    damageParticles.Stop();
	}

  void Update()
  {
    repairTimeLeft -= Time.deltaTime;

    if(repairTimeLeft <= 0)
    {
      repairTimeLeft = 0;
      cannonTargeting.Repair();
      damageParticles.Stop();
    }
  }
	
  void OnTriggerEnter2D(Collider2D other)
  {
    damageParticles.Play();
    cannonTargeting.Break();

    Destroy(other.gameObject);

    repairTimeLeft = repairTime;
  }
}
