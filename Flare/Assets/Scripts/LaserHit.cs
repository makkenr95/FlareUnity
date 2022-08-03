using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHit : MonoBehaviour
{
    public bool startLaserHitting = false;
    public bool stopLaserHitting = false;
    public GameObject laserHitEffectGO;
    public GameObject laserHitEffectGlow;
    public ParticleSystem laserHitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startLaserHitting)
        {
            laserHitEffect.Play();
            laserHitEffectGlow.SetActive(true);
            startLaserHitting = false;
        }
        if (stopLaserHitting)
        {
            laserHitEffect.Stop();
            laserHitEffectGlow.SetActive(false);
            stopLaserHitting = false;
        }
    }
}
