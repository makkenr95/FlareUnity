using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectsManager : MonoBehaviour
{
    public enum ItemName
    {
        LuxRadius

    }

    public List<ItemEffect> allItemEffects;
    List<ItemEffect> appliedItemEffects;
    private void Start()
    {
        appliedItemEffects = new List<ItemEffect>();
    }

    public void ApplyEffect(ItemEffect aItemEffect)
    {
        aItemEffect.ApplyEffect();
        appliedItemEffects.Add(aItemEffect);
    }
}
