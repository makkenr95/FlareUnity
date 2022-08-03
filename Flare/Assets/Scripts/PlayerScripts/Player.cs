using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public enum PlayerStat
    {
        ATTACKSPEED,
        ATTACKDAMAGE,
        ATTACKSPREAD,
        CRITCHANCE,
        CRITDAMAGE,
        HEALTH,
        HEALTHREGEN,
        SHIELD,
        SHIELDREGEN,
        MOVESPEED,
        PICKUPDISTANCE
    }
    public Dictionary<PlayerStat, float> myStatChanges = new Dictionary<PlayerStat, float>()
    {
        {PlayerStat.ATTACKSPEED, 0.2f},
        {PlayerStat.ATTACKDAMAGE, 2.0f},
        {PlayerStat.ATTACKSPREAD, 0.2f},
        {PlayerStat.CRITCHANCE, 0.2f},
        {PlayerStat.CRITDAMAGE, 0.2f},
        {PlayerStat.HEALTH, 0.2f},
        {PlayerStat.HEALTHREGEN, 0.2f},
        {PlayerStat.SHIELD, 0.2f},
        {PlayerStat.SHIELDREGEN, 0.2f},
        {PlayerStat.MOVESPEED, 0.2f},
        {PlayerStat.PICKUPDISTANCE, 0.3f}
    };
    public GameObject mySlider;

    public GameObject myExpProgress;

    public GameObject myLevelUpHUD;
    public LevelUpHUD myLevelUpHUDScript;

    public PlayerMovement myPlayerMovement;
    public PlayerWeapon myPlayerWeapon;
    public HealthComponent myHealthComponent;
    public PlayerAudioHandler playerAH;

    public GameObject glow;
    public GameObject sprite;
    public GameObject trail;

    public Inventory_SO inventorySO;

    PlayerStat myTargetStat;

    public int myLevel = 1;
    public float myXp = 0;
    public float myXpThreshold = 10;
    public float pickupDistance = 1;

    public bool hasTravelCrystal = false;

    void Awake()
    {
        myPlayerWeapon = GetComponent<PlayerWeapon>();
        myPlayerMovement = GetComponent<PlayerMovement>();
        myHealthComponent = GetComponent<HealthComponent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mySlider = GameObject.FindGameObjectWithTag("XPBar");
        myLevelUpHUD = GameObject.FindGameObjectWithTag("LevelUpHUD");
        ShowLevelUpHUD(false);

        if (myLevelUpHUD)
            myLevelUpHUDScript = myLevelUpHUD.GetComponent<LevelUpHUD>();

        //myExpProgress = GameObject.FindGameObjectWithTag("ExpProgress");
        if (myExpProgress)
            if (myExpProgress.activeSelf)
                myExpProgress.GetComponent<TextMeshProUGUI>().text = myXp + "/" + myXpThreshold;

    }

    // Update is called once per frame
    void Update()
    {
        mySlider = GameObject.FindGameObjectWithTag("XPBarSlider");
    }
    public void AddXP(float aValue)
    {
        myXp += aValue;
        if (myXp >= myXpThreshold)
        {
            LevelUp();
        }
        UpdateSlider();
    }
    void LevelUp()
    {
        ShowLevelUpHUD(true);

        if (myLevelUpHUDScript)
        {
            myLevelUpHUDScript.UpdateButtonInfos();
            Time.timeScale = 0.0f;
        }
    }
    void ShowLevelUpHUD(bool aValue)
    {
        if (myLevelUpHUD)
            myLevelUpHUD.SetActive(aValue);
    }
    void PostLevelUp()
    {
        myLevel++;
        Time.timeScale = 1.0f;
        ShowLevelUpHUD(false);
        myXp -= myXpThreshold;
        myXpThreshold += ((myXpThreshold * 0.5f) * 1.13f);
        myXpThreshold = Mathf.CeilToInt(myXpThreshold);
        UpdateSlider();
    }
    void UpdateSlider()
    {
        myExpProgress.GetComponent<TextMeshProUGUI>().text = myXp + "/" + myXpThreshold;
        mySlider.GetComponent<Slider>().value = Remap(myXp, 0, myXpThreshold, (float)0.0f, (float)1.0f);
    }
    public float Remap(float aValue, float aFrom1, float aTo1, float aFrom2, float aTo2)
    {
        return (aValue - aFrom1) / (aTo1 - aFrom1) * (aTo2 - aFrom2) + aFrom2;
    }
    void ChangeStatValue()
    {
        switch (myTargetStat)
        {
            case PlayerStat.ATTACKSPEED:
                {
                    myPlayerWeapon.attackSpeed += myStatChanges[PlayerStat.ATTACKSPEED];
                    break;
                }
            case PlayerStat.ATTACKDAMAGE:
                {
                    myPlayerWeapon.damage += myStatChanges[PlayerStat.ATTACKDAMAGE];
                    break;
                }
            case PlayerStat.ATTACKSPREAD:
                {
                    myTargetStat = PlayerStat.ATTACKSPEED;
                    break;
                }
            case PlayerStat.CRITCHANCE:
                {
                    myTargetStat = PlayerStat.ATTACKSPEED;
                    break;
                }
            case PlayerStat.CRITDAMAGE:
                {
                    myTargetStat = PlayerStat.ATTACKSPEED;
                    break;
                }
            case PlayerStat.HEALTH:
                {
                    myTargetStat = PlayerStat.ATTACKSPEED;
                    break;
                }
            case PlayerStat.HEALTHREGEN:
                {
                    myTargetStat = PlayerStat.ATTACKSPEED;
                    break;
                }
            case PlayerStat.SHIELD:
                {
                    myTargetStat = PlayerStat.ATTACKSPEED;
                    break;
                }
            case PlayerStat.SHIELDREGEN:
                {
                    myTargetStat = PlayerStat.ATTACKSPEED;
                    break;
                }
            case PlayerStat.MOVESPEED:
                {
                    myPlayerMovement.speed += myStatChanges[PlayerStat.MOVESPEED];
                    break;
                }
        }
        PostLevelUp();
    }
    public void Hide()
    {
        sprite.GetComponent<SpriteRenderer>().enabled = false;
        trail.SetActive(false);
        glow.SetActive(false);
    }
    public void Show()
    {
        sprite.GetComponent<SpriteRenderer>().enabled = true;
        trail.SetActive(true);
        glow.SetActive(true);
    }
    public void Disable()
    {
        myPlayerMovement.enabled = false;
        myPlayerWeapon.enabled = false;
    }
    public void Activate()
    {
        myPlayerMovement.enabled = true;
        myPlayerWeapon.enabled = true;
        myHealthComponent.enabled = true;
    }
    public void ChangeStat(string aPlayerStat)
    {
        switch (aPlayerStat)
        {
            case "ATTACKSPEED":
                {
                    myTargetStat = PlayerStat.ATTACKSPEED;
                    break;
                }
            case "ATTACKDAMAGE":
                {
                    myTargetStat = PlayerStat.ATTACKDAMAGE;
                    break;
                }
            case "ATTACKSPREAD":
                {
                    myTargetStat = PlayerStat.ATTACKSPREAD;
                    break;
                }
            case "CRITCHANCE":
                {
                    myTargetStat = PlayerStat.CRITCHANCE;
                    break;
                }
            case "CRITDAMAGE":
                {
                    myTargetStat = PlayerStat.CRITDAMAGE;
                    break;
                }
            case "HEALTH":
                {
                    myTargetStat = PlayerStat.HEALTH;
                    break;
                }
            case "HEALTHREGEN":
                {
                    myTargetStat = PlayerStat.HEALTHREGEN;
                    break;
                }
            case "SHIELD":
                {
                    myTargetStat = PlayerStat.SHIELD;
                    break;
                }
            case "SHIELDREGEN":
                {
                    myTargetStat = PlayerStat.SHIELDREGEN;
                    break;
                }
            case "MOVESPEED":
                {
                    myTargetStat = PlayerStat.MOVESPEED;
                    break;
                }
        }
        ChangeStatValue();
    }
}
