using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSeeker : ItemEffect
{
    // Start is called before the first frame update
    public override void ApplyEffect()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeapon>();
        player.isHoming = true;
        base.ApplyEffect();
    }
}
