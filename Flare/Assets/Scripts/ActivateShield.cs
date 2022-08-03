using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShield : ItemEffect
{
    public override void ApplyEffect()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var healthComp = player.GetComponent<HealthComponent>();

        healthComp.maxShield = 5;
        healthComp.InitShield();

        base.ApplyEffect();
    }
}
