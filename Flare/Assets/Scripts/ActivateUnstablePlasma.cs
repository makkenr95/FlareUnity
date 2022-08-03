using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUnstablePlasma : ItemEffect
{
    public override void ApplyEffect()
    {
        var playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeapon>();
        playerWeapon.hasUnstablePlasma = true;

        base.ApplyEffect();
    }
}
