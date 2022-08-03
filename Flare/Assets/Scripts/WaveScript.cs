using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    public AnimationCurve curve;
    public Vector3 topPos;
    public Vector3 lowPos;
    public float timer;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speed;
       
        transform.localPosition = Vector3.Lerp(lowPos, topPos, curve.Evaluate(timer));
        if (timer > 1.0f)
        {
            timer -= 1.0f;
        }
    }
}
