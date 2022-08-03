using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreenHandler : MonoBehaviour
{
    public TextMeshProUGUI timeNumberText;
    public TextMeshProUGUI enemiesDefeatedNumberText;

    public TextMeshProUGUI lightDefeatedNumberText;
    public TextMeshProUGUI heavyDefeatedNumberText;
    public TextMeshProUGUI fastDefeatedNumberText;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void UpdateInfo()
    {
        var enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        var enemyNum = 0;

        foreach (var key in enemySpawner.enemiesDefeated.Keys)
        {
            if (enemySpawner.enemiesDefeated.ContainsKey(key))
            {
                enemyNum += enemySpawner.enemiesDefeated[key];
            }
        }
        if (enemySpawner.enemiesDefeated.ContainsKey(EnemySpawner.EnemyType.Light))
        lightDefeatedNumberText.text = enemySpawner.enemiesDefeated[EnemySpawner.EnemyType.Light].ToString();
        if (enemySpawner.enemiesDefeated.ContainsKey(EnemySpawner.EnemyType.Heavy))
        heavyDefeatedNumberText.text = enemySpawner.enemiesDefeated[EnemySpawner.EnemyType.Heavy].ToString();
        if (enemySpawner.enemiesDefeated.ContainsKey(EnemySpawner.EnemyType.Fast))
        fastDefeatedNumberText.text = enemySpawner.enemiesDefeated[EnemySpawner.EnemyType.Fast].ToString();


        enemiesDefeatedNumberText.text = enemyNum.ToString();
        var hudHandler = GameObject.FindGameObjectWithTag("HUDHandler").GetComponent<HudHandler>();

        timeNumberText.text = hudHandler.minutes.ToString("00") + ":" + hudHandler.seconds.ToString("00") + ":" + Mathf.FloorToInt(hudHandler.milliseconds).ToString("00");
    }
}
