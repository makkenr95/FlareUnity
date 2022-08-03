using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpHUD : MonoBehaviour
{
    public GameObject attackSpeedButton;
    public GameObject attackDamageButton;
    public GameObject attackSpreadButton;

    public GameObject critChanceButton;
    public GameObject critDamageButton;

    public GameObject healthButton;
    public GameObject healthRegenButton;
    public GameObject shieldButton;
    public GameObject shieldRegenButton;

    public GameObject movementSpeedButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateButtonInfos()
    {
        attackSpeedButton.GetComponent<LevelUpButton>().UpdateButtonInfo();
        attackDamageButton.GetComponent<LevelUpButton>().UpdateButtonInfo();
        attackSpreadButton.GetComponent<LevelUpButton>().UpdateButtonInfo();

        critChanceButton.GetComponent<LevelUpButton>().UpdateButtonInfo();
        critDamageButton.GetComponent<LevelUpButton>().UpdateButtonInfo();

        healthButton.GetComponent<LevelUpButton>().UpdateButtonInfo();
        healthRegenButton.GetComponent<LevelUpButton>().UpdateButtonInfo();
        shieldButton.GetComponent<LevelUpButton>().UpdateButtonInfo();
        shieldRegenButton.GetComponent<LevelUpButton>().UpdateButtonInfo();

        movementSpeedButton.GetComponent<LevelUpButton>().UpdateButtonInfo();
    }
}
