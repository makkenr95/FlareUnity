using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingAlpha : MonoBehaviour
{
    public AnimationCurve curve;
    public SpriteRenderer sR;

    public float timer;
    public float speed;
    public bool looping = true;

    float startAlpha;
    float alpha;

    [Range(0, 1)]
    public float strength;
    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        alpha = sR.color.a;
        startAlpha = alpha;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 1.0f)
            timer += Time.deltaTime * speed;

        alpha = startAlpha + curve.Evaluate(timer) * strength;
        if (timer >= 1.0f && looping)
        {
            timer -= 1.0f;
        }
        sR.color = new Color(sR.color.r, sR.color.g, sR.color.b, alpha);
    }
}
