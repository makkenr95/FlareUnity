using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShotgun : ItemEffect
{
    public override void ApplyEffect()
    {
        var playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeapon>();

        playerWeapon.projectileAmount += 2;
        playerWeapon.projectileSpread += 0.05f;

        base.ApplyEffect();
    }
}
