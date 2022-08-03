using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLaser : ItemEffect
{
    public override void ApplyEffect()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            var playerWeapon = player.GetComponent<PlayerWeapon>();

            if (playerWeapon != null)
            {
                playerWeapon.laserActive = true;
            }
        }
        base.ApplyEffect();
    }
}
