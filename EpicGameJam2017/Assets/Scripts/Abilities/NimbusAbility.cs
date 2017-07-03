using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NimbusAbility : Ability
{
    /// <summary>
    /// Duration of flight in seconds.
    /// </summary>
    public const float AbilityDuration = 5f;
    public const float SpeedBonus = 2f;

    public void Cast(Unicorn caster)
    {
        caster.StartCoroutine(RunAbility(caster));
    }

    private IEnumerator RunAbility(Unicorn caster)
    {
        var nimbus = caster.SpawnNimbus();
        yield return new WaitForSeconds(nimbus.animationDuration + nimbus.waitDuration);
        caster.SpeedForce = caster.speedForce * SpeedBonus; // 100% faster, whueeeee!
        caster.SetFlying(true);
        yield return new WaitForSeconds(AbilityDuration);
        caster.SpeedForce = caster.speedForce;
        caster.SetFlying(false);
        yield return nimbus.FlyAway();
    }
}
