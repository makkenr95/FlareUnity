using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerWeapon : MonoBehaviour
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
    public EventSystem eventSystem;
    [Space]

    public bool isAutoAiming = true;
    public bool isAutoShooting = true;
    [Space]

    public float projectileSpeed = 1;
    public float projectileSpread = 1;
    public float damage = 0;
    float weaponCooldown = 1;
    float weaponCooldownProgress = 1;
    public float attackSpeed = 1;
    [Space]

    public float piercingChance = 0;
    public float maxPiercingAmount = 0;
    [Space]

    public int projectileAmount = 1;
    [Space]

    public GameObject laserPrefab;
    GameObject laser;
    public bool laserActive = false;
    public float laserDamageScale = 0.1f;
    [Space]

    public List<GameObject> whiteDwarfs;
    public bool dwarfActive = false;
    [Space]

    public bool isHoming = true;
    public float homingStrength = 0.1f;
    [Space]

    public GameObject unstablePlasma;
    public bool hasUnstablePlasma = true;
    float unstablePlasmaCooldown = 1;
    float unstablePlasmaCooldownProgress = 1;
    public float unstablePlasmaAttackSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        laser = Instantiate(laserPrefab);
        laser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemySpawner)
        {
            enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        }
        if (Input.GetMouseButton(0))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var go in raycastResults)
                {
                    Debug.Log(go.gameObject.name, go.gameObject);
                }
            }
        }
        if (Time.deltaTime > 0.0f)
            Shooting();
    }
    void Shooting()
    {
        UpdateAttackCooldowns();
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
                FireProjectile(projectileAmount);
        }
        else
        {
            //SHOOT PROJECTILES
            if (Input.GetMouseButton(0))
            {
                FireProjectile(projectileAmount);
            }
            //TURNS OFF LASER
            if (laserActive && laser.activeSelf)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    laser.SetActive(false);
                    var playerAH = GetComponent<Player>().playerAH;
                    playerAH.StopSound();
                    playerAH.PlayOneShot(playerAH.laserEnd);
                }
            }
        }
    }
    void FireProjectile(int amount)
    {
        //ACTIVATES AND HANDLES LASER
        if (laserActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                laser.SetActive(true);
                laser.GetComponent<LaserHit>().laserHitEffect.Stop();
                var playerAH = GetComponent<Player>().playerAH;
                playerAH.PlayOneShot(playerAH.laserStart);
                playerAH.PlaySound(playerAH.laserLoop, true);
            }
            FireLaser();
        }
        if (hasUnstablePlasma)
        {
            if (unstablePlasmaCooldownProgress < 0.0f)
            {
                Instantiate(unstablePlasma, transform.position, transform.rotation);
                unstablePlasmaCooldownProgress = unstablePlasmaCooldownProgress + unstablePlasmaCooldown;
            }
        }

        if (weaponCooldownProgress < 0.0f)
        {
            var playerAH = GetComponent<Player>().playerAH;
            for (int i = 0; i < amount; i++)
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
                Instantiate<GameObject>(projectile, transform.position, transform.rotation);
                playerAH.PlayOneShot(playerAH.shootProjectile);
            }
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

                    hitGameObject.GetComponent<HealthComponent>().ApplyDamage((damage * (attackSpeed * Time.deltaTime)) * laserDamageScale);

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
            if (enemy)
            {
                if (enemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    return true;
                }
            }
            else
            {
                enemySpawner.GetComponent<EnemySpawner>().enemies.Remove(enemy);
                break;
            }
        }
        return false;
    }
    void UpdateAttackCooldowns()
    {
        if (weaponCooldownProgress > 0.0f)
        {
            if (Time.deltaTime * attackSpeed < 1.0f)
                weaponCooldownProgress -= Time.deltaTime * attackSpeed;
            else
                weaponCooldownProgress -= 1;
        }

        if (unstablePlasmaCooldownProgress > 0.0f)
        {
            if (Time.deltaTime * unstablePlasmaAttackSpeed < 1.0f)
                unstablePlasmaCooldownProgress -= Time.deltaTime * unstablePlasmaAttackSpeed;
            else
                unstablePlasmaCooldownProgress -= 1;
        }
    }
}
