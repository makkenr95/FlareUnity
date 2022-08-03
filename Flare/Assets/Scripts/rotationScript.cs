using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationScript : MonoBehaviour
{
    GameObject player;

    public Vector3 rotation;
    public float angle;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if ((new Vector2(player.transform.position.x, player.transform.position.y) - 
             new Vector2(transform.position.x, transform.position.y)).magnitude > 15)
        angle += Time.deltaTime * speed;   
        transform.RotateAround(rotation, speed);
    }
}
