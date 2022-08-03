using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transporter : MonoBehaviour
{
    public GameObject blueStar;
    public GameObject player;
    public EnemySpawner spawner;
    public GameObject playerHUD;
    public ParticleSystem hyperSpeedParticleSystem;
    public Material skybox;
    public Image vignette;
    public Image transportVignette;
    public Material currentSkybox;
    public IntroAudioHandler introAH;

    public AnimationCurve vignetteCurve;
    public float vignetteTimer;
    public float vignetteSpeed;
    public bool startFlash = false;
    public bool flashActive = false;
    public bool hyperspaceExited = false;

    public AnimationCurve curve;
    public Cinemachine.CinemachineVirtualCamera cam;

    public Vector3 offset;

    public float speed;
    public float transportTimer;
    public float hyperSpaceTimer;
    public float hyperSpaceStartTime;
    public float hyperSpaceEndTime;

    public bool startTransport = false;
    public bool startHyperSpace = false;
    public bool inHyperSpace = false;
    public bool transporting = false;

    // Start is called before the first frame update
    void Start()
    {
        //blueStar.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHyperSpaceFlash();
        if (inHyperSpace)
        {
            UpdateHyperSpace();
        }

        if (startTransport)
        {
            var transposer = cam.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
            //transposer.m_XDamping = 0;
            //transposer.m_YDamping = 0;
            //transposer.m_ZDamping = 0;
            //blueStar.SetActive(true);
            //blueStar.transform.position = player.transform.position + offset;


            transportTimer = 0;
            transporting = true;
            startTransport = false;
            player.GetComponent<Player>().hasTravelCrystal = false;
        }
        if (transporting)
        {
            transportTimer += Time.deltaTime * speed;
            //player.transform.position = new Vector3(blueStar.transform.position.x * curve.Evaluate(transportTimer), player.transform.position.y, player.transform.position.z);

            if (!inHyperSpace)
            {
                if (transportTimer >= hyperSpaceStartTime && transportTimer <= hyperSpaceEndTime)
                {
                    startFlash = true;
                    StartHyperSpace();
                }
            }

            if (transportTimer >= 1.0f)
            {
                var transposer = cam.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
                transposer.m_XDamping = 1;
                transposer.m_YDamping = 1;
                transposer.m_ZDamping = 1;
                transporting = false;
            }
        }
        if (startFlash)
        {
            flashActive = true;
            transportVignette.color = Color.white;
            startFlash = false;
        }
        if (flashActive)
        {
            Flash();
        }
    }
    void UpdateHyperSpace()
    {
        player.transform.position += new Vector3(10, 0, 0) * Time.deltaTime;
        int i = currentSkybox.shader.FindPropertyIndex("_Rotation");
        currentSkybox.SetFloat("_Rotation", currentSkybox.GetFloat("_Rotation") + Time.deltaTime * 200);
        vignette.color = new Color(1, 1, 1, 0.1f);
        if (transportTimer >= hyperSpaceEndTime)
        {
            ExitHyperSpace();
        }
    }
    void UpdateHyperSpaceFlash()
    {
        //var flashTimer = Remap(hyperSpaceTimer, 0.0f, flashLength, 0.0f, 1.0f);
    }
    void StartTransport()
    {

    }
    void ExitTransport()
    {

    }
    void StartHyperSpace()
    {
        hyperSpeedParticleSystem.Play();
        introAH.PlayAudioClip(introAH.hyperspaceLoop);
        inHyperSpace = true;
    }
    void ExitHyperSpace()
    {
        startTransport = false;
        startHyperSpace = false;
        playerHUD.SetActive(true);
        transporting = false;
        hyperSpeedParticleSystem.Stop();
        inHyperSpace = false;
        startFlash = true;
        hyperspaceExited = true;
        vignette.color = new Color(1, 1, 1, 0);
        player.GetComponent<Player>().Show();
        GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>().spawnerActive = true;
        introAH.PlayAudioClip(introAH.hyperSpaceEnd);
        player.GetComponent<PlayerStartBehavior>().ActivatePlayer();
    }
    void BreakHyperSpaceEffect()
    {

    }
    public float Remap(float aValue, float aFrom1, float aTo1, float aFrom2, float aTo2)
    {
        return (aValue - aFrom1) / (aTo1 - aFrom1) * (aTo2 - aFrom2) + aFrom2;
    }
    void Flash()
    {
        vignetteTimer += Time.deltaTime * vignetteSpeed;
        var progress = vignetteCurve.Evaluate(vignetteTimer);

        transportVignette.color = new Color(1, 1, 1, progress);
        //vignette.color = new Color(1, 1, 1, progress);

        if (vignetteTimer >= 1)
        {
            flashActive = false;
            vignetteTimer = 0;
        }
    }
}
