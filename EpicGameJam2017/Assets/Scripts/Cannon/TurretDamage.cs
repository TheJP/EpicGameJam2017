using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TurretDamage : MonoBehaviour
{
    [Tooltip("The time it takes to repair a turret")]
    public float repairTime = 5.0f;

    [Tooltip("Sound effect that is played, when turret is damaged")]
    public AudioClip turretDamageSound;

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

        if (repairTimeLeft <= 0)
        {
            repairTimeLeft = 0;
            cannonTargeting.Repair();
            damageParticles.Stop();
        }
    }

    public void Damage()
    {
        damageParticles.Play();
        cannonTargeting.Break();
        GetComponent<AudioSource>().PlayOneShot(turretDamageSound);

        repairTimeLeft = repairTime;
    }
}
