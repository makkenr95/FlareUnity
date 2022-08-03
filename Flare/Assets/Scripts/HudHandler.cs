using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudHandler : MonoBehaviour
{
    public GameObject constellationHUD;

    public Inventory_SO inventory;

    public TextMeshProUGUI redText;
    public TextMeshProUGUI yellowText;

    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI maxEnemiesText;
    public TextMeshProUGUI currentEnemiesText;
    public TextMeshProUGUI fpsText;

    public TextMeshProUGUI minutesText;

    public float timer = 0;
    public int minutes = 0;
    public int seconds = 0;
    public float milliseconds = 0;

    public float fpsHudInterval = 0.25f;
    public float fpsHudIntervalTimer = 0;

    public bool changeDebugHUD = false;
    public bool debugHUD = true;


    // Start is called before the first frame update
    void Start()
    {
        constellationHUD = GameObject.FindGameObjectWithTag("ConstellationHUD");
        constellationHUD.SetActive(false);

        redText.text = inventory.redCrystals.amount.ToString();
        yellowText.text = inventory.yellowCrystals.amount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        milliseconds = timer * 100.0f;

        if (milliseconds >= 100.0f)
        {
            seconds++;
            milliseconds -= 100.0f;
            timer -= 1.000f;
        }
        if (seconds >= 60)
        {
            minutes++;
            seconds -= 60;
        }

        UpdateTimerHUD();
        UpdateFPSHUD();

        UpdateDebugHUD();
    }
    public void UpdateResourcesHUD()
    {
        redText.text = inventory.redCrystals.amount.ToString();
        yellowText.text = inventory.yellowCrystals.amount.ToString();
    }
    public void UpdateTimerHUD()
    {
        minutesText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + Mathf.FloorToInt(milliseconds).ToString("00");
    }
    public void UpdateFPSHUD()
    {
        fpsHudIntervalTimer += Time.unscaledDeltaTime;
        if (fpsHudIntervalTimer > fpsHudInterval)
        {
            fpsText.text = Mathf.FloorToInt(1 / Time.unscaledDeltaTime) + " FPS";
            fpsHudIntervalTimer = 0;
        }
    }
    public void UpdateDebugHUD()
    {
        if (changeDebugHUD && !debugHUD)
        {
            difficultyText.gameObject.SetActive(true);
            maxEnemiesText.gameObject.SetActive(true);
            currentEnemiesText.gameObject.SetActive(true);
            fpsText.gameObject.SetActive(true);
            changeDebugHUD = false;
        }
        else if (changeDebugHUD && debugHUD)
        {
            difficultyText.gameObject.SetActive(false);
            maxEnemiesText.gameObject.SetActive(false);
            currentEnemiesText.gameObject.SetActive(false);
            fpsText.gameObject.SetActive(false);
            changeDebugHUD = false;
        }
    }
}
