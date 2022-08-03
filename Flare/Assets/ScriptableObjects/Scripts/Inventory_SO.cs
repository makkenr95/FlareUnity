using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "Inventory/New Inventory")]
public class Inventory_SO : ScriptableObject
{
    public Resource_SO redCrystals;
    public Resource_SO yellowCrystals;
}
