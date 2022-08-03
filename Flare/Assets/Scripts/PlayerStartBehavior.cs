using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerStartBehavior : MonoBehaviour
{
    public Player player;
    public GameObject sun;
    public CinemachineVirtualCamera virtualCamera;
    Vector3 startFollowOffset;

    bool movingPlayerPhase = false;
    bool transportingPlayerPhase = false;

    public AnimationCurve moveCurve;
    Vector2 playerMoveStartPos;
    public Vector2 playerMoveLength;
    float moveTimer = 0;
    public float moveSpeed = 1;

    public AnimationCurve preTransportCurve;
    Vector2 playerPreTransportStartPos;
    public Vector2 playerPreTransportLength;
    float preTransportTimer = 0;
    public float preTransportSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.Hide();
        player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingPlayerPhase)
        {
            if (playerMoveStartPos == Vector2.zero)
            {
                playerMoveStartPos = transform.position;
                var transposer = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
                transposer.m_XDamping = 3;
                transposer.m_YDamping = 3;
                transposer.m_ZDamping = 3;
            }
            MovingPlayerPhase();
        }
        if (transportingPlayerPhase)
        {
            if (playerPreTransportStartPos == Vector2.zero)
            {
                playerPreTransportStartPos = transform.position;
            }
            TransportingPhase();
        }
    }
    public void ActivatePlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.Show();
        player.Activate();

        player.trail.SetActive(false);

        SetCameraFollow(player);
        movingPlayerPhase = true;
    }
    void SetCameraFollow(Player player)
    {
        var transposer = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
        virtualCamera.transform.position = player.transform.position + new Vector3(0, 0, -10);
        transposer.m_FollowOffset = new Vector3(0, 0, -10);

        virtualCamera.Follow = player.transform;
    }
    void MovingPlayerPhase()
    {
        moveTimer += Time.deltaTime * moveSpeed;
        var progress = moveCurve.Evaluate(moveTimer);

        var transposer = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();

        var damp = Mathf.Lerp(10, 2, progress);

        transposer.m_XDamping = damp;
        transposer.m_YDamping = damp;
        transposer.m_ZDamping = damp;

        transform.position = playerMoveStartPos + playerMoveLength * progress;

        if (moveTimer >= 1)
        {
            movingPlayerPhase = false;
            transportingPlayerPhase = true;
        }
    }
    void TransportingPhase()
    {
        preTransportTimer += Time.deltaTime * preTransportSpeed;
        var progress = preTransportCurve.Evaluate(preTransportTimer);

        transform.position = playerPreTransportStartPos + playerPreTransportLength * progress;

        var transposer = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();

        var damp = Mathf.Lerp(2, 0.3f, progress);

        transposer.m_XDamping = damp;
        transposer.m_YDamping = damp;
        transposer.m_ZDamping = damp;

        if (preTransportTimer >= 1)
        {
            transposer.m_XDamping = 0.8f;
            transposer.m_YDamping = 0.8f;
            transposer.m_ZDamping = 0.8f;
            transportingPlayerPhase = false;
            //sun.SetActive(false);
            player.trail.SetActive(false);
            var transporter = GameObject.FindGameObjectWithTag("Transporter").GetComponent<Transporter>();
            transporter.startTransport = true;
            enabled = false;
        }
    }
}
