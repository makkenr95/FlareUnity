using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float range;
    public float damage;

    public GameObject area;
    public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        area.transform.localScale = new Vector3(range, range, 0);
        //Debug.DrawLine(transform.position, transform.position + new Vector3(range / 2, 0, 0), Color.red, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
