using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip laserStart, laserLoop, laserEnd;
    public AudioClip shieldRecharged, shieldDepleted;
    public AudioClip playerDeath;
    public AudioClip shootProjectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void PlaySound(AudioClip clip, bool looping)
    {
        audioSource.loop = looping;
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void StopSound()
    {
        audioSource.Stop();
    }
}
