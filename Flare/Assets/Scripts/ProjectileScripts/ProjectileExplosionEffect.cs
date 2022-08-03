using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosionEffect : MonoBehaviour
{
    public List<Sprite> sprites;
    public AnimationCurve curve;
    public float timer = 0;
    public float speed = 1;
    public float scale = 1;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        transform.localScale = new Vector3(curve.Evaluate(timer) * scale, curve.Evaluate(timer) * scale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 1.0f)
        {
            Destroy(gameObject);
            return;
        }
        timer += Time.deltaTime * speed;
        transform.localScale = new Vector3(curve.Evaluate(timer) * scale, curve.Evaluate(timer) * scale, 1);

    }
}
