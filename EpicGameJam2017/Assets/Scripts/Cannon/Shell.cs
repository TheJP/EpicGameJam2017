﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [Tooltip("Distance at which the bullet will explode into fragments")]
    public float distanceToExplode = 2;

    public Material ParticleMaterial;
    public Material ParticleTrailMaterial;

    public Players Player;

    private Vector3 target;
    private float speed;
    private ParticleSystem ps;
    private GameObject shell3D;
    private bool shellIsExploding;

    void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        if(!ps) print("child needs Shell Particle Component");
        shell3D = transform.Find("Shell3D").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - target).sqrMagnitude > distanceToExplode*distanceToExplode)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            return;
        }

        // BOOM!
        if (!shellIsExploding)
        {
            shellIsExploding = true;
            shell3D.SetActive(false);
            PlayParticleSystem();
        }

        if (shellIsExploding && !ps.IsAlive()) Cleanup();
    }

    public void Cleanup()
    {
        Destroy(gameObject);
    }

    public void Goto(Vector3 target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }

    public void PlayParticleSystem()
    {
        ps.Play();
    }
}

