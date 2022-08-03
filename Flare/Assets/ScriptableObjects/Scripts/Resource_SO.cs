using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Resource/New Resource")]
public class Resource_SO : ScriptableObject
{
    public Pickup_SO pickup;
    public int amount;
}
