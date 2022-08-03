using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDwarf : ItemEffect
{
    public GameObject dwarf;
    public override void ApplyEffect()
    {
        dwarf.SetActive(true);
        base.ApplyEffect();
    }
}
