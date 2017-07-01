using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public SpriteRenderer abilityIcon;
    public Sprite toothpickSprite;
    public Sprite nimbbusSprite;

    private Ability ability = null;

    public bool HasAbility
    {
        get { return ability != null; }
    }

    /// <summary>Cast ability and use it up in the process.</summary>
    public void CastAbility(Unicorn caster)
    {
        if(ability == null) { return; }
        ability.Cast(caster);
        ability = null;
    }

    /// <summary>Sets the toothpick ability into this slot if it is free.</summary>
    public bool SetToothpickAbility()
    {
        return SetAbility(new ToothpickAbility(), toothpickSprite);
    }

    public bool SetAbility(Ability ability, Sprite abilityIcon)
    {
        if(this.ability != null || ability == null || abilityIcon == null) { return false; }
        this.ability = ability;
        this.abilityIcon.sprite = abilityIcon;
        return true;
    }
}
