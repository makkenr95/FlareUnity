using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickup", menuName = "Pickups/New Pickup")]
public class Pickup_SO : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public Sprite glowSprite;
    public GameObject pickup;
    public Vector2 worldSize = new Vector2(1, 1);
    public Vector2 iconSize = new Vector2(1, 1);
}
