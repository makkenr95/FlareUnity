using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLasers : MonoBehaviour
{
    public GameObject laserPrefab;
    public float rotationSpeed = 10;

    public List<GameObject> lasers = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        transform.position = transform.parent.position;
    }
}
