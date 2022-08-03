using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudioHandler : MonoBehaviour

{
    //public List<AudioClip> hyperspaceClips = new List<AudioClip>();
    public AudioClip introStart, hyperspaceLoop, hyperSpaceEnd;
    public AudioSource hyperspaceAudioSource; 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PlayAudioClip(AudioClip clip)
    {
        hyperspaceAudioSource.clip = clip;
        hyperspaceAudioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
