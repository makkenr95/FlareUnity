using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpButton : MonoBehaviour
{
    public Player.PlayerStat myStat;

    public TextMeshProUGUI description;
    public TextMeshProUGUI clarification;
    public TextMeshProUGUI currentNumber;
    public TextMeshProUGUI increaseNumber;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateButtonInfo()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        switch (myStat)
        {
            case Player.PlayerStat.ATTACKSPEED:
                {
                    currentNumber.text = player.myPlayerWeapon.attackSpeed.ToString("0.00");
                    increaseNumber.text = (player.myPlayerWeapon.attackSpeed + player.myStatChanges[myStat]).ToString("0.00");
                    break;
                }
            case Player.PlayerStat.ATTACKDAMAGE:
                {
                    currentNumber.text = player.myPlayerWeapon.damage.ToString("0");
                    increaseNumber.text = (player.myPlayerWeapon.damage + player.myStatChanges[myStat]).ToString("0");
                    break;
                }
            case Player.PlayerStat.ATTACKSPREAD:
                {
                    currentNumber.text = player.myPlayerWeapon.projectileSpread.ToString("0.00");
                    increaseNumber.text = (player.myPlayerWeapon.projectileSpread + player.myStatChanges[myStat]).ToString("0.00");
                    break;
                }
            case Player.PlayerStat.CRITCHANCE:
                {
                    //currentNumber.text = player.myPlayerWeapon.damage.ToString("0");
                    //increaseNumber.text = (player.myPlayerWeapon.damage + player.myStatChanges[myStat]).ToString("0");
                    break;
                }
            case Player.PlayerStat.CRITDAMAGE:
                {
                    //currentNumber.text = player.myPlayerWeapon.damage.ToString("0");
                    //increaseNumber.text = (player.myPlayerWeapon.damage + player.myStatChanges[myStat]).ToString("0");
                    break;
                }
            case Player.PlayerStat.HEALTH:
                {
                    //currentNumber.text = player.myPlayerWeapon.damage.ToString("0");
                    //increaseNumber.text = (player.myPlayerWeapon.damage + player.myStatChanges[myStat]).ToString("0");
                    break;
                }
            case Player.PlayerStat.HEALTHREGEN:
                {
                    //currentNumber.text = player.myPlayerWeapon.damage.ToString("0");
                    //increaseNumber.text = (player.myPlayerWeapon.damage + player.myStatChanges[myStat]).ToString("0");
                    break;
                }
            case Player.PlayerStat.SHIELD:
                {
                    //currentNumber.text = player.myPlayerWeapon.damage.ToString("0");
                    //increaseNumber.text = (player.myPlayerWeapon.damage + player.myStatChanges[myStat]).ToString("0");
                    break;
                }
            case Player.PlayerStat.SHIELDREGEN:
                {
                    //currentNumber.text = player.myPlayerWeapon.damage.ToString("0");
                    //increaseNumber.text = (player.myPlayerWeapon.damage + player.myStatChanges[myStat]).ToString("0");
                    break;
                }
            case Player.PlayerStat.MOVESPEED:
                {
                    currentNumber.text = player.myPlayerMovement.speed.ToString("0.0");
                    increaseNumber.text = (player.myPlayerMovement.speed + player.myStatChanges[myStat]).ToString("0.0");
                    break;
                }
        }
    }
}
