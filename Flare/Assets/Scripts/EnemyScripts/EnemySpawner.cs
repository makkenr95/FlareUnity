using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum EnemyType
    {
        Light,
        Fast,
        Heavy,
        MiniBoss,
        NONE
    }

    public Dictionary<EnemyType, int> enemiesDefeated = new Dictionary<EnemyType, int>();

    GameObject player;
    GameObject hUDHandler;
    public GameObject miniBoss;
    [Space]

    public GameObject lightEnemy, lightPatternEnemy,fastEnemy, heavyEnemy, laserEnemy, homingEnemy;
    //NOT INCLUDING MINIBOSS
    public int numEnemyTypes;

    // SET TO AMOUNT OF ENEMY TYPES -1
    public int maxSpawnStrength = 1;

    public Vector2 spawnDirection;
    public float spawnDistance = 10;
    public float spawnTimer = 0;
    public float minSpawnTime = 1;
    public float maxSpawnTime = 2;
    public float spawnTime = 1;
    public float miniBossSpawnIntervall = 180;
    public float miniBossSpawnTimer = 0;
    public float difficulty = 0;
    public float difficultyMultiplier = 0;
    public int spawnValue = 1;
    public int maxTotalEnemyAmount = 20;
    int maxEnemyAmount = 20;

    public bool spawnerActive = false;

    public List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hUDHandler = GameObject.FindGameObjectWithTag("HUDHandler");
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnerActive)
        {
            return;
        }
        var sceneEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Count < sceneEnemies.Length)
        {
            foreach (var enemy in sceneEnemies)
            {
                if (!enemies.Contains(enemy))
                {
                    enemies.Add(enemy);
                }
            }
        }

        difficulty += Time.deltaTime * difficultyMultiplier;
        spawnTimer += Time.deltaTime;
        miniBossSpawnTimer += Time.deltaTime;
        maxEnemyAmount = Mathf.CeilToInt(difficulty * 0.75f);

        if (miniBossSpawnTimer > miniBossSpawnIntervall)
        {
            Spawn(miniBoss);
            miniBossSpawnTimer = 0;
        }

        if (enemies.Count < maxTotalEnemyAmount)
            if (spawnTimer > spawnTime && maxEnemyAmount > (float)enemies.Count)
            {
                int spawnStrength = Random.Range(0, numEnemyTypes);
                switch (spawnStrength)
                {
                    case 0:
                        {
                            Spawn(lightEnemy);
                            break;
                        }
                    case 1:
                        {
                            Spawn(heavyEnemy);
                            break;
                        }
                    case 2:
                        {
                            Spawn(fastEnemy);
                            break;
                        }
                    case 3:
                        {
                            Spawn(laserEnemy);
                            break;
                        }
                }
                if (spawnValue <= 0)
                {
                    spawnTimer = 0;
                    spawnValue = Mathf.FloorToInt(difficulty * 0.5f);
                    spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                    maxSpawnStrength = Mathf.FloorToInt(difficulty * 0.5f);
                }
            }
        UpdateHUD();
    }
    private void Spawn(GameObject enemy)
    {
        if (difficulty < enemy.GetComponent<EnemyMovement>().myMinDifficulty)
            return;

        spawnDirection.x = Random.Range(-1.0f, 1.0f);
        spawnDirection.y = Random.Range(-1.0f, 1.0f);

        spawnDirection.Normalize();

        enemy.transform.position = player.transform.position;
        enemy.transform.position += new Vector3(spawnDirection.x * spawnDistance,
                                                     spawnDirection.y * spawnDistance,
                                                     0);

        enemies.Add(Instantiate<GameObject>(enemy));
        spawnValue -= enemy.GetComponent<EnemyMovement>().mySpawnValue;
    }
    public void UpdateHUD()
    {
        var hUDHandlerScript = hUDHandler.GetComponent<HudHandler>();
        hUDHandlerScript.difficultyText.text = "Difficulty: " + Mathf.CeilToInt(difficulty).ToString();
        hUDHandlerScript.maxEnemiesText.text = "Max Enemies: " + Mathf.CeilToInt(maxEnemyAmount).ToString();
        hUDHandlerScript.currentEnemiesText.text = "Current Enemies: " + Mathf.CeilToInt(enemies.Count).ToString();
    }
    public void EnemyDefeated(EnemyType type)
    {
        if (enemiesDefeated.ContainsKey(type))
        {
            enemiesDefeated[type] += 1;
        }
        else
        {
            enemiesDefeated.Add(type, 1);
        }
    }
}
