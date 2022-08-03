using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingGlow : MonoBehaviour
{
    SpriteRenderer sprite;
    public AnimationCurve curve;

    public float timer;
    public float speed;

    [Range(0,1)]
    public float strength;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speed;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, curve.Evaluate(timer) * strength);
        if (timer >= 1.0f)
        {
            timer -= 1.0f;
        }
    }
}
