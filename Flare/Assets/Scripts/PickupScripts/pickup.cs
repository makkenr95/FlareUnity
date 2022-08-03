using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public enum PickupType
    {
        CONSTELLATION,
        TRAVEL,
        XP,
        RED,
        YELLOW
    }
    public float rotationSpeed = 1;
    public float minDistance = 6;
    public float attraction = -1;
    public float attractionSpeed = 1;
    public float maxSpeed = 1;
    public float pickupDistance = 0.05f;
    public float xpGain = 1;
    public AnimationCurve curve;
    public Inventory_SO inventory;
    public PickupType pickupType;

    GameObject myPlayer;
    Player myPlayerComp;
    Transporter myTransporter;
    public GameObject myConstellationHUD;

    Vector3 startPos;
    Vector3 playerStartPos;


    public float timer = 0;
    bool moving = false;

    private void Awake()
    {
        //myConstellationHUD = GameObject.FindGameObjectWithTag("ConstellationHUD");
    }

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        myPlayerComp = myPlayer.GetComponent<Player>();
        myTransporter = GameObject.FindGameObjectWithTag("Transporter").GetComponent<Transporter>();
        startPos = transform.position;
        playerStartPos = myPlayer.transform.position;
        myConstellationHUD = GameObject.FindGameObjectWithTag("MainLinker").GetComponent<MainLinker>().ContellationHUD;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Move();
    }
    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }
    void Move()
    {
        if (moving)
        {
            if (attraction < 10.0f)
                attraction += (Time.deltaTime * attractionSpeed) / ((myPlayer.transform.position - transform.position).magnitude) / 2;

            var translation = ((myPlayer.transform.position - transform.position).normalized * attraction) * Time.deltaTime;
            transform.position += translation;
            if ((myPlayer.transform.position - transform.position).magnitude < pickupDistance)
            {
                switch (pickupType)
                {
                    case PickupType.CONSTELLATION:
                        {
                            myConstellationHUD.SetActive(true);
                            myConstellationHUD.GetComponent<ConstellationListHandler>().UpdateList();
                            Time.timeScale = 0;
                            break;
                        }
                    case PickupType.TRAVEL:
                        {
                            myPlayerComp.hasTravelCrystal = true;
                            myTransporter.startTransport = true;
                            break;
                        }
                    case PickupType.RED:
                        {
                            inventory.redCrystals.amount++;
                            break;
                        }
                    case PickupType.YELLOW:
                        {
                            inventory.yellowCrystals.amount++;
                            break;
                        }
                    case PickupType.XP:
                        {
                            myPlayerComp.AddXP(xpGain);
                            break;
                        }
                }
                GameObject.FindGameObjectWithTag("HUDHandler").GetComponent<HudHandler>().UpdateResourcesHUD();
                Destroy(gameObject);
                Destroy(this);
            }
        }
        else
        {
            float distance = Vector3.Magnitude(myPlayer.transform.position - transform.position);
            // USING PLAYER STAT PICKUPDISTANCE FOR NOW. MIGHT CHANGE TO INDIVIDUAL PICKUP DISTANCE PER PICKUP
            if (distance < myPlayerComp.pickupDistance)
            {
                moving = true;
            }
        }

    }
}
