using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserHit : MonoBehaviour
{
    PolygonCollider2D col;
    public GameObject laserHitEffect;
    public GameObject laserHitEffectGlow;
    public float damage = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<PolygonCollider2D>();
        laserHitEffect.GetComponent<ParticleSystem>().Stop();
        laserHitEffectGlow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            laserHitEffect.GetComponent<ParticleSystem>().Play();
            laserHitEffectGlow.SetActive(true);
            collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(damage);
            laserHitEffect.transform.position = collision.ClosestPoint(transform.position);
            var direction = (collision.transform.position - transform.position).normalized;
            var directionAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            laserHitEffect.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            laserHitEffect.GetComponent<ParticleSystem>().Stop();
            laserHitEffectGlow.SetActive(false);
        }
    }
    //private void OnCollisionEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(damage);
    //        laserHitEffect.transform.position = collision.ClosestPoint(transform.position);
    //        var direction = (collision.transform.position - transform.position).normalized;
    //        var directionAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //        laserHitEffect.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
    //    }
    //}
}
