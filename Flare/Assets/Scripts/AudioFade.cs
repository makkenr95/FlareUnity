using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    public AnimationCurve fadeCurve;

    public AudioSource audioSource;

    public float speed;
    public float progress;

    public float startVolume;
    public float endVolume;

    public bool fading;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            progress += Time.deltaTime * speed;
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, fadeCurve.Evaluate(progress));
        }
    }
}
