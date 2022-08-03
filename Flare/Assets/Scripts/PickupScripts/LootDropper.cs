using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    public Pickup_SO myPickup;
    public GameObject myXPPickup;
    public int myMinAmount = 1;
    public int myMaxAmount = 1;
    public float myDropPositionOffsetRange;
    [Range(0,1)]
    public float myDropChance = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TryDropLoot()
    {
        float roll = Random.Range(0.0f,1.0f);
        if (roll <= myDropChance)
        {
            DropLoot();
        }
        DropXP();
    }
    void DropLoot()
    {
        GameObject pickup = Instantiate<GameObject>(myPickup.pickup, transform.position, transform.rotation);
    }
    void DropXP()
    {
        GameObject xpPickup = Instantiate<GameObject>(myXPPickup, transform.position + new Vector3(
                                                                                                    Random.Range(-myDropPositionOffsetRange, myDropPositionOffsetRange),
                                                                                                    Random.Range(-myDropPositionOffsetRange, myDropPositionOffsetRange), 0), 
                                                                                                        Quaternion.Euler(new Vector3(0,0,Random.Range(-50, 50))));
        xpPickup.GetComponent<pickup>().xpGain = GetComponent<EnemyMovement>().xpGain;
    }
}
