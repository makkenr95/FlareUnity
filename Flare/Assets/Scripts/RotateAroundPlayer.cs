using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour
{

    public GameObject player;
    public float speed = 1;
    public float distance = 1;

    float tempDistance = 1;
    float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + new Vector3(distance, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (distance != tempDistance)
        {
            transform.position = player.transform.position + new Vector3(distance, 0, 0);
        }
        tempDistance = distance;

        transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), speed);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
