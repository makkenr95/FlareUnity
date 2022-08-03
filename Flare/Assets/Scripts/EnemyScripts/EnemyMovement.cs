using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rB;

    public GameObject player;
    public GameObject graphics;
    public Vector2 direction;
    public Vector2 velocity;
    public Vector2 targetVelocity;
    public Vector2 previousTargetVelocity;
    public Vector2 playerSpeed;
    public float breakStrength = 1;
    public float manuverability = 1;
    public float maxSpeed = 1;
    float speed = 0;
    public float wantedDistance = 0;
    public float wantedDistanceMargin = 0;
    public float acceleration = 1;
    public float directionProgress = 0;
    public float velocityProgress = 0;
    public float breakProgress = 0;
    public bool isInputing;
    public float xpGain = 1;
    public float myMinDifficulty = 0;
    public int mySpawnValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rB = GetComponent<Rigidbody2D>();
        rB.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDirection();
        CalculateVelocity();
        MovePlayer();
    }
    private void CalculateVelocity()
    {
        var currentDistance = (player.transform.position - transform.position).magnitude;
        if (currentDistance > wantedDistance + wantedDistanceMargin)
        {
            if (speed < maxSpeed)
                speed += acceleration * Time.deltaTime;
        }
        else if (currentDistance < wantedDistance - wantedDistanceMargin)
        {
            if (speed > -maxSpeed)
            {
                speed -= acceleration * Time.deltaTime;
            }
        }
        else
        {
            if (speed > 0.0f)
                speed -= breakStrength * Time.deltaTime;
        }
    }

    private void CalculateDirection()
    {
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }

    void MovePlayer()
    {
        rB.velocity = (direction * speed);
    }
}
// OLD FROM MOVEPLAYER()
//var directionAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//if (graphics)
//graphics.transform.rotation = Quaternion.Euler(0, 0, directionAngle - 90);

//transform.Translate((direction * speed) * Time.deltaTime);

//GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 1000.0f);
