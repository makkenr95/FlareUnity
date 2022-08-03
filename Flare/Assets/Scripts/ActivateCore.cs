using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCore : ItemEffect
{
    public override void ApplyEffect()
    {
        var playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();

        playerHealth.IncreaseMaxHealth(5);

        base.ApplyEffect();
    }
}
