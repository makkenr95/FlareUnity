using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ProjectileMovement : MonoBehaviour
{
    EnemySpawner enemySpawner;
    public GameObject impactEffect;

    public GameObject impactEffectTemp;
    public enum ProjectileOwner
    {
        Player,
        Enemy
    }
    public enum ProjectileType
    {
        Normal,
        Homing,
        Piercing,
        Exploding,
        Chain
    }
    [SerializeField]
    public ProjectileType projectileType;
    [SerializeField]
    public ProjectileOwner projectileOwner;

    GameObject player;
    GameObject target;
    Rigidbody2D rb;

    public Vector2 targetPos;
    public Vector2 direction;

    public float speed = 1;
    public float manuverability = 1;
    public float spread = 1;
    public float damage = 1;
    public float piercingChance = 0;
    public float maxPiercingAmount = 0;
    public float currentPiercingAmount = 0;
    public float homingStrength = 0;

    public float targetFindTimer = 0;
    public float targetFindTime = 0.33f;

    public bool isAutoAiming = true;
    public bool isHoming = true;

    public float projectileSpeedVariation = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        //Randomize speed a tiny amount (0.01f = pretty noticable)
        speed += (Random.Range(-speed, speed) * projectileSpeedVariation);
        switch (projectileOwner)
        {
            case ProjectileOwner.Player:
                {
                    enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
                    if (isAutoAiming)
                    {
                        target = FindClosestEnemy();
                        if (target)
                            targetPos = target.transform.position;
                    }
                    else
                    {
                        targetPos = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                        Debug.DrawLine(targetPos, transform.position, new Color(1, 0, 0, 1), 0.5f);
                    }
                    break;
                }
            case ProjectileOwner.Enemy:
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                    target = player;
                    targetPos = target.transform.position;
                    break;
                }
        }
        direction = targetPos - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        direction = new Vector2(direction.x + Random.Range(-spread, spread), direction.y + Random.Range(-spread, spread));
        direction.Normalize();
        if (isHoming)
        {
            transform.up = direction;
            rb = GetComponent<Rigidbody2D>();
            rb.freezeRotation = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<SpriteRenderer>().isVisible)
        {
            Destroy(gameObject);
        }
        switch (projectileOwner)
        {
            case ProjectileOwner.Player:
                {
                    ProjUpdate();
                    break;
                }
            case ProjectileOwner.Enemy:
                {
                    ProjUpdate();
                    break;
                }
        }
    }
    void ProjUpdate()
    {
        if (isHoming)
        {
            target = FindTarget();
            if (target != null)
            {

                direction = target.transform.position - transform.position;
                direction.Normalize();

                var rotateAmount = Vector3.Cross((Vector2)direction, transform.up).z;

                rb.angularVelocity = -rotateAmount * homingStrength;
            }

            rb.velocity = transform.up * speed;
        }
        else
        {
            transform.Translate((direction * speed) * Time.deltaTime);
        }
    }
    GameObject FindClosestEnemy()
    {
        float shortestDistance = 10000;
        float distance = 0;
        GameObject closestEnemy = null;

        if (enemySpawner == null)
        {
            enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        }

        foreach (GameObject enemy in enemySpawner.enemies)
        {
            if (enemy == null || !enemy.activeSelf)
            {
                enemySpawner.enemies.Remove(enemy);
                break;
            }
            distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
            if (shortestDistance < 0.1f)
                return closestEnemy;
        }
        return closestEnemy;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (projectileType)
        {
            case ProjectileType.Normal:
                {
                    if (collision.gameObject.GetComponent<HealthComponent>())
                    {
                        if (collision.gameObject.GetComponent<HealthComponent>().type == HealthComponent.HealthType.Enemy && projectileOwner == ProjectileOwner.Player)
                        {
                            if (collision.gameObject.activeSelf)
                            {
                                impactEffectTemp = Instantiate<GameObject>(impactEffect);
                                impactEffectTemp.transform.position = transform.position;
                            }
                            //impactEffectTemp.GetComponent<ParticleSystem>().Play();
                        }
                    }
                    break;
                }
        }
        switch (projectileOwner)
        {
            case ProjectileOwner.Player:
                {
                    if (collision.gameObject.GetComponent<HealthComponent>())
                    {
                        if (collision.gameObject.GetComponent<HealthComponent>().type == HealthComponent.HealthType.Enemy)
                        {
                            PlayerProjectileCollision(collision);
                            TryDestroy();
                        }
                    }
                    break;
                }
            case ProjectileOwner.Enemy:
                {
                    if (collision.gameObject.GetComponent<HealthComponent>())
                    {
                        if (collision.gameObject.GetComponent<HealthComponent>().type == HealthComponent.HealthType.Player)
                        {
                            EnemyProjectileCollision(collision);
                            TryDestroy();
                        }
                    }
                    break;
                }
        }
    }
    private void PlayerProjectileCollision(Collider2D collision)
    {
        switch (projectileType)
        {
            case ProjectileType.Normal:
                {
                    if (collision.gameObject.GetComponent<HealthComponent>())
                    {
                        collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(damage);
                    }
                    break;
                }
        }
    }
    private void EnemyProjectileCollision(Collider2D collision)
    {
        switch (projectileType)
        {
            case ProjectileType.Normal:
                {
                    if (collision.gameObject.GetComponent<HealthComponent>())
                    {
                        collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(damage);
                    }
                    break;
                }
        }
    }
    private void TryDestroy()
    {
        var roll = Random.Range(0.0f, 1.0f);
        if (roll < piercingChance)
        {
            if (maxPiercingAmount > currentPiercingAmount)
            {
                currentPiercingAmount++;
                return;
            }
        }
        Destroy(gameObject);
    }
    private GameObject FindTarget()
    {
        targetFindTimer += Time.deltaTime;
        if (targetFindTimer > targetFindTime)
        {
            targetFindTimer = 0;
            return FindClosestEnemy();
        }
        return target;
    }
}
