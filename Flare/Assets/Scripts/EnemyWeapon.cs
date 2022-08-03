using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public enum WeaponType
    {
        Normal,
        Shotgun
    }
    public GameObject projectile;
    public GameObject enemySpawner;
    public GameObject player;

    public ProjectileMovement.ProjectileType projectileType;
    public WeaponType weaponType;

    public bool isAutoAiming = true;
    public bool isAutoShooting = true;

    public float weaponCooldown = 1;
    public float weaponCooldownProgress = 1;
    public float attackSpeed = 1;
    public float projectileSpeed = 1;
    public float homingSpeed = 1;
    public float projectileSpread = 1;
    public float damage = 0;
    public float piercingChance = 0;
    public float maxPiercingAmount = 0;
    public float shotgunProjectileAmount = 3;
    public float range = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemySpawner)
        {
            enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        }
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

        if ((player.transform.position - transform.position).magnitude < range)
        {
            if (weaponType == WeaponType.Shotgun)
            {
                FireProjectiles(shotgunProjectileAmount);
            }
            else
                FireProjectile();
        }
    }
    void FireProjectile()
    {
        if (weaponCooldownProgress < 0.0f)
        {
            projectile.GetComponent<ProjectileMovement>().projectileOwner = ProjectileMovement.ProjectileOwner.Enemy;
            projectile.GetComponent<ProjectileMovement>().speed = projectileSpeed;
            projectile.GetComponent<ProjectileMovement>().damage = damage;
            projectile.GetComponent<ProjectileMovement>().spread = projectileSpread;
            projectile.GetComponent<ProjectileMovement>().piercingChance = piercingChance;
            projectile.GetComponent<ProjectileMovement>().maxPiercingAmount = maxPiercingAmount;
            projectile.GetComponent<ProjectileMovement>().isAutoAiming = isAutoAiming;
            projectile.GetComponent<ProjectileMovement>().projectileType = projectileType;
            Instantiate<GameObject>(projectile, transform.position, new Quaternion(0, 0, 0, 0));
            weaponCooldownProgress = weaponCooldownProgress + weaponCooldown;
        }
    }
    void FireProjectiles(float aAmount)
    {
        if (weaponCooldownProgress < 0.0f)
        {
            for (int i = 0; i < aAmount; i++)
            {
                projectile.GetComponent<ProjectileMovement>().projectileOwner = ProjectileMovement.ProjectileOwner.Enemy;
                projectile.GetComponent<ProjectileMovement>().speed = projectileSpeed;
                projectile.GetComponent<ProjectileMovement>().damage = damage;
                projectile.GetComponent<ProjectileMovement>().spread = projectileSpread;
                projectile.GetComponent<ProjectileMovement>().piercingChance = piercingChance;
                projectile.GetComponent<ProjectileMovement>().maxPiercingAmount = maxPiercingAmount;
                projectile.GetComponent<ProjectileMovement>().isAutoAiming = isAutoAiming;
                projectile.GetComponent<ProjectileMovement>().projectileType = projectileType;
                Instantiate<GameObject>(projectile, transform.position, new Quaternion(0, 0, 0, 0));
            }
            weaponCooldownProgress = weaponCooldownProgress + weaponCooldown;
        }
    }
}
