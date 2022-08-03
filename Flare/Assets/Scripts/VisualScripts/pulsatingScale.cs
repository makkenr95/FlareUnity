using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulsatingScale : MonoBehaviour
{
    public AnimationCurve curve;

    public float timer;
    public float speed;
    public bool looping = true;

    Vector3 startScale;

    [Range(0, 1)]
    public float strength;
    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 1.0f)
        timer += Time.deltaTime * speed;

        transform.localScale = new Vector3(startScale.x + curve.Evaluate(timer) * strength, 
                                            startScale.y + curve.Evaluate(timer) * strength, 1);
        if (timer >= 1.0f && looping)
        {
            timer -= 1.0f;
        }
    }
}
