using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject joystick;
    public Vector2 inputDirection;
    public Vector2 direction;
    public Vector2 velocity;
    public Vector2 targetVelocity;
    public Vector2 previousTargetVelocity;
    public Vector2 playerSpeed;
    public float breakStrength = 1;
    public float manuverability = 1;
    public float speed = 1;
    public float acceleration = 1;
    public float directionProgress = 0;
    public float velocityProgress = 0;
    public float breakProgress = 0;

    public float wormholeDistance = 1;
    public float wormholeTimer = 0;
    public float wormholeTime = 1;

    public bool hasWormhole;
    public bool isInputing;

    // Start is called before the first frame update
    void Start()
    {
        wormholeTimer = wormholeTime;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDirection();
        CalculateVelocity();
        MovePlayer();

        if (hasWormhole)
        {
            if (wormholeTimer < wormholeTime)
            {
                wormholeTimer += Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Blink();
            }
        }
    }

    private void CalculateVelocity()
    {
        if (inputDirection != Vector2.zero)
        {
            breakProgress = 0;

            if (velocityProgress < 1.0f)
            {
                velocityProgress += Time.deltaTime * acceleration;

                if (velocityProgress > 1.0f)
                    velocityProgress = 1.0f;
            }

            targetVelocity = direction * playerSpeed;

            if (targetVelocity != previousTargetVelocity)
            {
                velocityProgress = 0.0f;
            }

            velocity = Vector2.Lerp(velocity, targetVelocity, velocityProgress);

            previousTargetVelocity = targetVelocity;
        }
        else
        {
            if (breakProgress < 1.0f)
            {
                breakProgress += Time.deltaTime * breakStrength;

                if (breakProgress > 1.0f)
                    breakProgress = 1.0f;
            }
            velocity = Vector2.Lerp(velocity, Vector2.zero, breakProgress);
            direction = Vector2.Lerp(direction, Vector2.zero, breakProgress);
        }
    }
    private void Blink()
    {
        if (wormholeTimer >= wormholeTime)
        {
            if (direction != Vector2.zero)
            {
                transform.Translate(direction.normalized * wormholeDistance);
                wormholeTimer = 0;
            }
        }
    }
    private void CalculateDirection()
    {
        //TOUCH MOVEMENT
        {
            /*
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == Input.touches[0].fingerId)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        Vector2 joystickPos = joystick.transform.position;
                        direction = (touch.position - joystickPos).normalized;
                    }
                }
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 joystickPos = joystick.transform.position;

                Debug.Log("Mouse Y: " + (int)Input.mousePosition.y);
                Debug.Log("Mouse X: " + (int)Input.mousePosition.x);

                direction = ((Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)) - joystickPos).normalized;
                velocity = direction * playerSpeed;
                directionProgress = 0;
            }
            else
            {
                if (velocity.magnitude > 0.00f)
                {
                    directionProgress += Time.deltaTime * breakStrength;
                    velocity = Vector2.Lerp(direction, new Vector2(0, 0), directionProgress) * playerSpeed;
                }
            }
            */
        } //INACTIVE

        //WASD MOVEMENT
        {
            //Input handling
            inputDirection = new Vector2(0, 0);
            isInputing = false;
            if (Input.GetKey(KeyCode.W))
            {
                if (!Input.GetKey(KeyCode.S))
                {
                    inputDirection.y = 1;
                    isInputing = true;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (!Input.GetKey(KeyCode.D))
                {
                    inputDirection.x = -1;
                    isInputing = true;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (!Input.GetKey(KeyCode.W))
                {
                    inputDirection.y = -1;
                    isInputing = true;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (!Input.GetKey(KeyCode.A))
                {
                    inputDirection.x = 1;
                    isInputing = true;
                }
            }
            if (isInputing)
            {
                //Lerp direction progression
                if (directionProgress < 1.0f)
                {
                    directionProgress += Time.deltaTime * manuverability;
                    if (directionProgress > 1.0f)
                    {
                        directionProgress = 1.0f;
                    }
                }

                //New direction calculation
                if (inputDirection != Vector2.zero)
                    inputDirection = inputDirection.normalized;
                direction = Vector2.Lerp(direction, inputDirection, directionProgress);
            }
            else
            {
                directionProgress = 0;
            }
        }
    }

    void MovePlayer()
    {
        //transform.Translate((velocity * speed) * Time.deltaTime);

        GetComponent<Rigidbody2D>().velocity = velocity * speed;
        //GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 1000.0f);
    }
}
