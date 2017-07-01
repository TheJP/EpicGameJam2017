using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleShell : MonoBehaviour
{

    public Players? Player = null;
    private ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        var customData = ps.customData;
        customData.enabled = true;
        var color = Player.HasValue ? Constants.PlayerColors[Player.Value] : Constants.defaultColor;

        customData.SetMode(ParticleSystemCustomData.Custom1, UnityEngine.ParticleSystemCustomDataMode.Color);
        customData.SetColor(ParticleSystemCustomData.Custom1, color);

    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    //void OnParticleCollision(GameObject other)
    //{
    //    print("ps side collisioin");
    //}

    //void OnParticleTrigger()
    //{
    //    print("ps side trigger");
    //}
}
