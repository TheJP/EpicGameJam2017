using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShell : MonoBehaviour
{

    public Players Player = Players.None;
    private ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
       

    }

    public void Play(Players Player)
    {
        var customData = ps.customData;
        customData.enabled = true;
        var color = Constants.PlayerColors[Player];

        customData.SetMode(ParticleSystemCustomData.Custom1, UnityEngine.ParticleSystemCustomDataMode.Color);
        customData.SetColor(ParticleSystemCustomData.Custom1, color);
        ps.Play();
    }
}
