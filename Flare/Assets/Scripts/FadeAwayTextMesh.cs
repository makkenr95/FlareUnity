using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeAwayTextMesh : MonoBehaviour
{
    public TextMeshProUGUI text;

    public AnimationCurve fadeCurve;
    public float startAlpha = 1;
    public float speed;
    public float timer;

    public bool destroyOnDone = true;
    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, startAlpha);
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
        text.color = new Color(text.color.r, text.color.g, text.color.b, fadeCurve.Evaluate(timer));

    }
}
