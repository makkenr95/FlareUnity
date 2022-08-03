using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/New Character")]
public class Player_SO : ScriptableObject
{
    public string characterName;
    public string description;

    public int maxHealth;
    public int maxShield;
    public int healthRegenSpeed;
    public int shieldRegenSpeed;

    public float attackSpeed;
    public float attackDamage;
    public float attackSpread;

    public float movementSpeed;

    public Vector2 size;
    public Sprite sprite;
}
