using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWormhole : ItemEffect
{
    public override void ApplyEffect()
    {
        var playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        
        playerMove.hasWormhole = true;

        base.ApplyEffect();
    }
}
