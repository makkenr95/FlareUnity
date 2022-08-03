using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthComponent : MonoBehaviour
{
    public EnemySpawner.EnemyType enemyType;

    public GameObject constellationPickup;
    public GameObject deathEffect;
    public GameObject healthBarPrefab;
    public GameObject healthBar;
    public GameObject shieldBarPrefab;
    public GameObject shieldBar;
    public GameObject shieldGraphic;
    public GameObject deathScreen;
    public GameObject deathScreenPanel;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldText;

    public Vector3 healthBarOffset;
    public Vector3 shieldBarOffset;
    public bool isMiniBoss = false;

    bool stopShieldGraphic = false;

    float stopProgress = 1;

    public enum HealthType
    {
        Player,
        Enemy
    }

    public HealthType type;

    public float health;
    public float maxHealth;

    float shieldRegenRound;
    public float shieldRegen;
    public float shield;
    public float maxShield;

    public float shieldCooldown = 1;
    public float shieldCooldownTimer;

    private void Awake()
    {
        if (type == HealthType.Player)
        {
            if (deathScreenPanel)
            deathScreenPanel.SetActive(true);
            if (deathScreen)
            deathScreen.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitHealth();
        InitShield();
    }

    // Update is called once per frame
    void Update()
    {
        ShieldRegen();
        if (shield > 0.0f && !shieldGraphic.activeSelf)
        {
            shieldGraphic.SetActive(true);
            var particles = shieldGraphic.GetComponentsInChildren<ParticleSystem>();
            foreach (var particleSystem in particles)
            {
                particleSystem.Play();
            }
            shieldBar.GetComponent<Slider>().value = shield / maxShield;
            stopProgress = particles[0].startLifetime;
        }

        if (type == HealthType.Enemy)
        {
            if (healthBar)
                healthBar.transform.position = transform.position + healthBarOffset;
            if (shieldBar)
                shieldBar.transform.position = transform.position + healthBarOffset + shieldBarOffset;
        }
        if (healthText)
        {
            healthText.text = health.ToString() + "/" + maxHealth.ToString();
        }
        if (shieldText)
        {
            shieldText.text = shield.ToString() + "/" + maxShield.ToString();
        }
        if (stopShieldGraphic)
        {
            StopShieldGraphic();
        }
    }
    public void ApplyDamage(float value)
    {
        //SHIELD
        if (shield > 0.0f)
        {
            shieldCooldownTimer = 0;
            value -= shield;
            shield -= value + shield;
            if (shield <= 0.0f)
            {
                shieldBar.SetActive(false);
                stopShieldGraphic = true;
            }
        }

        if (shield < maxShield)
        {
            shieldBar.SetActive(true);
            shieldBar.GetComponent<Slider>().value = shield / maxShield;
        }

        if (value <= 0.0f)
        {
            return;
        }

        //HEALTH
        health -= value;
        if (health < 1.0f)
        {
            OnDeath();
        }
        if (health < maxHealth)
        {
            healthBar.SetActive(true);
            healthBar.GetComponent<Slider>().value = health / maxHealth;
        }
    }
    private void OnDeath()
    {
        if (type == HealthType.Enemy)
        {
            EnemySpawner spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
            RewardPlayer();

            spawner.EnemyDefeated(enemyType);

            spawner.enemies.Remove(gameObject);
            if (deathEffect)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }
            Destroy(healthBar);
            Destroy(gameObject);
        }
        if (type == HealthType.Player)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var playerMovement = player.GetComponent<PlayerMovement>();
            var playerWeapon = player.GetComponent<PlayerWeapon>();

            playerMovement.enabled = false;
            playerWeapon.enabled = false;

            deathScreen.SetActive(true);
            deathScreenPanel.SetActive(true);
            deathScreenPanel.GetComponent<DeathScreenHandler>().UpdateInfo();

            player.GetComponent<Player>().Hide();
            Time.timeScale = 0;
            this.enabled = false;
        }
    }
    public void Kill()
    {
        OnDeath();
    }
    void RewardPlayer()
    {
        GetComponent<LootDropper>().TryDropLoot();
    }
    void InitHealth()
    {
        if (type == HealthType.Enemy)
        {
            healthBar = Instantiate(healthBarPrefab, GameObject.FindGameObjectWithTag("HUDHandler").transform);
            healthBar.transform.position += healthBarOffset;
            healthBar.SetActive(false);
        }
        health = maxHealth;
        healthBar.GetComponent<Slider>().value = health / maxHealth;
        if (healthText)
        {
            healthText.text = health.ToString() + "/" + maxHealth.ToString();
        }
    }
    public void InitShield()
    {
        if (type == HealthType.Enemy)
        {
            shieldBar = Instantiate(shieldBarPrefab, GameObject.FindGameObjectWithTag("HUDHandler").transform);
            shieldBar.transform.position += shieldBarOffset;
        }
        shieldBar.SetActive(false);

        if (maxShield > 0)
        {
            if (shieldGraphic != null)
                shieldGraphic.SetActive(true);

            shield = maxShield;

            shieldBar.SetActive(true);
            shieldBar.GetComponent<Slider>().value = shield / maxShield;

            var particles = shieldGraphic.GetComponentsInChildren<ParticleSystem>();
            stopProgress = particles[0].startLifetime;
        }

        if (shieldText)
        {
            shieldText.text = shield.ToString() + "/" + maxShield.ToString();
        }
    }
    void StopShieldGraphic()
    {
        stopProgress -= Time.deltaTime;

        var particles = shieldGraphic.GetComponentsInChildren<ParticleSystem>();
        foreach (var particleSystem in particles)
        {
            particleSystem.Stop();
        }

        if (stopProgress <= 0.0f)
        {
            if (shieldGraphic != null)
                shieldGraphic.SetActive(false);

            stopShieldGraphic = false;
        }

    }
    void ShieldRegen()
    {
        if (shield < maxShield)
        {
            if (shieldCooldownTimer < shieldCooldown)
                shieldCooldownTimer += Time.deltaTime;

            if (shieldCooldownTimer >= shieldCooldown)
                shieldRegenRound += shieldRegen * Time.deltaTime;

            if (shieldRegenRound >= 1)
            {
                shieldRegenRound -= 1;
                shield += 1;

                shieldBar.GetComponent<Slider>().value = shield / maxShield;
            }
        }
    }
    public void IncreaseMaxHealth(int value)
    {
        health *= ((maxHealth + value) / maxHealth);
        health = Mathf.FloorToInt(health);
        maxHealth += value;
        healthBar.GetComponent<Slider>().value = health / maxHealth;
    }
}
