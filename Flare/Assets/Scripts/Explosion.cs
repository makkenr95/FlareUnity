using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float range;
    public float damage;
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        var explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        explosion.GetComponent<ExplosionEffect>().range = range;
        explosion.GetComponent<ExplosionEffect>().damage = damage;

        RaycastHit2D[] hits;
        hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.forward);
        
        foreach(var hit in hits)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.GetComponent<HealthComponent>().ApplyDamage(damage);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
