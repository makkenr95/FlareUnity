using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlasma : MonoBehaviour
{
    public Rigidbody2D areaRB;
    public GameObject projectileExplosion;

    public float attraction = -1;
    public float attractionSpeed = 1;
    public float maxSpeed = 1;
    public AnimationCurve curve;

    public GameObject target;

    Vector3 startPos;
    Vector3 targetStartPos;


    public float timer = 0;
    public bool moving = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (target)
            {
                if (attraction < 5.0f)
                    attraction += (Time.deltaTime * attractionSpeed) / ((target.transform.position - transform.position).magnitude) / 2;
            }
            else
            {
                Explode();
                return;
            }

            var translation = ((target.transform.position - transform.position).normalized * attraction) * Time.deltaTime;
            transform.position += translation;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Explode();
        }
    }
    void Explode()
    {
        Instantiate(projectileExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
