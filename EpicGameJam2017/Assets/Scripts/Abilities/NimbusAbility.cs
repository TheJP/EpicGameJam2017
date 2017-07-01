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

    private Unicorn caster;

    public void Cast(Unicorn caster)
    {
        // TODO: Visualize nimbus broom!
        this.caster = caster;
        caster.SpeedForce = caster.speedForce * SpeedBonus; // 100% faster, whueeeee!
        caster.StartCoroutine(End().GetEnumerator());
    }

    private IEnumerable End()
    {
        yield return new WaitForSeconds(AbilityDuration);
        caster.SpeedForce = caster.speedForce;
    }
}
