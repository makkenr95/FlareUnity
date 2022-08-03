using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAwayImage : MonoBehaviour
{
    public Image image;

    public AnimationCurve fadeCurve;
    public float startAlpha = 1;
    public float speed;
    public float timer;

    public bool destroyOnDone = true;
    // Start is called before the first frame update
    void Start()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, startAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 1.0f)
        {
            if (destroyOnDone)
                Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime * speed;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, fadeCurve.Evaluate(timer));

    }
}
