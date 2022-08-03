using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlasmaArea : MonoBehaviour
{
    public UnstablePlasma uP;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            uP.moving = true;
            uP.target = collision.gameObject;
        }
    }
}
