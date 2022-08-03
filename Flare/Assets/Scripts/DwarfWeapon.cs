using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfWeapon : MonoBehaviour
{
    public enum WeaponType
    {
        Normal,
        Homing,
        Piercing,
        Exploding,
        Chain
    }
    public GameObject projectile;
    public GameObject enemySpawner;
    public GameObject laserPrefab;
    GameObject laser;

    public bool isAutoAiming = true;
    public bool isAutoShooting = true;

    public float weaponCooldown = 1;
    public float weaponCooldownProgress = 1;
    public float attackSpeed = 1;
    public float projectileSpeed = 1;
    public float projectileSpread = 1;
    public float damage = 0;
    public float piercingChance = 0;
    public float maxPiercingAmount = 0;
    public float range = 1;
    public float homingStrength = 0;

    public bool isHoming = false;
    public bool laserActive = false;
    public bool dwarfActive = false;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        if (laserPrefab)
        {
            laser = Instantiate(laserPrefab);
            laser.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemySpawner)
        {
            enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        }
        if (Time.deltaTime > 0.0f)
            Shooting();
    }
    void Shooting()
    {
        if (weaponCooldownProgress > 0.0f)
        {
            if (Time.deltaTime * attackSpeed < 1.0f)
                weaponCooldownProgress -= Time.deltaTime * attackSpeed;
            else
                weaponCooldownProgress -= 1;
        }
        if (isAutoShooting)
        {
            if (enemySpawner)
            {
                if (enemySpawner.GetComponent<EnemySpawner>().enemies.Count == 0)
                    return;
            }
            else
                return;

            if (IsEnemiesOnScreen())
            {
                if ((FindClosestEnemy().transform.position - transform.position).magnitude < range)
                {
                    FireProjectile();
                }
            }
        }
        else
        {
            //SHOOT PROJECTILES
            if (Input.GetMouseButton(0))
            {
                FireProjectile();
            }
            //TURNS OFF LASER
            if (laserActive && laser.activeSelf)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    laser.SetActive(false);
                }
            }
        }
    }
    void FireProjectile()
    {
        //ACTIVATES AND HANDLES LASER
        if (laserActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                laser.SetActive(true);
                laser.GetComponent<LaserHit>().laserHitEffect.Stop();
            }
            FireLaser();
            return;
        }

        if (weaponCooldownProgress < 0.0f)
        {
            projectile.GetComponent<ProjectileMovement>().projectileOwner = ProjectileMovement.ProjectileOwner.Player;
            projectile.GetComponent<ProjectileMovement>().speed = projectileSpeed;
            projectile.GetComponent<ProjectileMovement>().damage = damage;
            projectile.GetComponent<ProjectileMovement>().spread = projectileSpread;
            projectile.GetComponent<ProjectileMovement>().piercingChance = piercingChance;
            projectile.GetComponent<ProjectileMovement>().maxPiercingAmount = maxPiercingAmount;
            projectile.GetComponent<ProjectileMovement>().isAutoAiming = isAutoAiming;
            projectile.GetComponent<ProjectileMovement>().isHoming = isHoming;
            projectile.GetComponent<ProjectileMovement>().homingStrength = homingStrength;
            var proj = Instantiate<GameObject>(projectile, transform.position, transform.rotation);
            proj.transform.localScale *= 0.8f; 
            var playerAH = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().playerAH;
            playerAH.PlayOneShot(playerAH.shootProjectile);
            weaponCooldownProgress = weaponCooldownProgress + weaponCooldown;
        }
    }
    void FireLaser()
    {
        float distance = 20;
        var lineLaser = laser.GetComponent<LineRenderer>();
        //GLOW MUST BE CHILD 0
        var lineGlow = laser.transform.GetChild(0).GetComponent<LineRenderer>();

        var targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        targetPos.z = transform.position.z;
        var direction = targetPos - new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)));

        lineLaser.positionCount = 2;
        lineLaser.SetPosition(0, gameObject.transform.position);
        lineGlow.positionCount = 2;
        lineGlow.SetPosition(0, gameObject.transform.position);

        lineGlow.SetPosition(1, new Vector3(direction.normalized.x, direction.normalized.y, 0) * distance + transform.position);
        lineLaser.SetPosition(1, new Vector3(direction.normalized.x, direction.normalized.y, 0) * distance + transform.position);

        //RAYCAST TOWARD DIRECTION
        var hitEnemy = false;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction.normalized);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                var hitGameObject = hit.transform.gameObject;
                Debug.Log(hitGameObject);

                if (hitGameObject.tag == "Enemy")
                {
                    hitEnemy = true;
                    distance = Mathf.Abs((hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude);
                    lineGlow.SetPosition(1, hit.point);
                    lineLaser.SetPosition(1, hit.point);
                    laser.GetComponent<LaserHit>().startLaserHitting = true;

                    var directionAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    laser.GetComponent<LaserHit>().laserHitEffectGO.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
                    laser.GetComponent<LaserHit>().laserHitEffectGO.transform.position = hit.point;

                    hitGameObject.GetComponent<HealthComponent>().ApplyDamage(damage * (attackSpeed * Time.deltaTime));

                    break;
                }
            }
        }
        if (!hitEnemy)
        {
            laser.GetComponent<LaserHit>().stopLaserHitting = true;
        }
    }
    bool IsEnemiesOnScreen()
    {
        foreach (var enemy in enemySpawner.GetComponent<EnemySpawner>().enemies)
        {
            if (enemy == null)
            {
                enemySpawner.GetComponent<EnemySpawner>().enemies.Remove(enemy);
                break;
            }
            if (enemy.activeSelf)
            {
                if (enemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    return true;
                }
            }
        }
        return false;
    }
    GameObject FindClosestEnemy()
    {
        float shortestDistance = 10000;
        float distance = 0;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemySpawner.GetComponent<EnemySpawner>().enemies)
        {
            if (enemy == null || !enemy.activeSelf)
            {
                enemySpawner.GetComponent<EnemySpawner>().enemies.Remove(enemy);
                break;
            }
            distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
            if (!enemy.GetComponent<SpriteRenderer>().isVisible)
                continue;
            if (shortestDistance < 0.1f)
                return closestEnemy;
        }
        return closestEnemy;
    }
}
