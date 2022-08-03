using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Constellation", menuName = "Constellation/New Constellation")]
public class Constellation_SO : ScriptableObject
{
    public ItemEffectsManager.ItemName itemEffect;

    public Sprite image;
    public string title;
    public string desctiption;
}
