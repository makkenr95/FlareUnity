using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Populator : MonoBehaviour
{
    public GameObject[] grass;
    public GameObject[] trees;
    public GameObject[] rocks;
    public GameObject[,] squares;
    public Vector2 gridSize;
    public Vector2 squareSize;
    public Vector2 spacing;
    public Vector2 offset;
    public float scale;
    public bool update = false;
    public float sortingOffset;

    // Start is called before the first frame update
    void Start()
    {
        squares = new GameObject[(int)gridSize.x, (int)gridSize.y];
        PlaceGrid(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            PlaceGrid(false);
            update = false;
        }
    }
    void PlaceGrid(bool init)
    {
        SpriteRenderer sR = null;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                int rndEntityType = Random.Range(0, 11);
                int rndEntity = Random.Range(0, 5);
                int rnd = Random.Range(1, 100);
                Vector2 individualOffset = new Vector2(Random.Range(-0.75f, 0.75f), Random.Range(-0.75f, 0.75f));
                if (init)
                {
                    squares[x, y] = Instantiate<GameObject>(GetEntity(rndEntityType, rndEntity));
                    sR = squares[x, y].GetComponent<SpriteRenderer>();
                    if (rnd >= 50)
                    {
                        sR.flipX = true;
                    }
                }

                squares[x, y].transform.position = new Vector2(x * scale, y * scale) + offset + new Vector2(spacing.x * x, spacing.y * y) + individualOffset;
                sR.sortingOrder = -(int)((squares[x, y].transform.position.y * 1000.0f) - sortingOffset);
            }
        }
    }
    GameObject GetEntity(int rndEntityType, int rndEntity)
    {
        if (rndEntityType >= 10)
            return trees[rndEntity];
        else if(rndEntityType >= 9)
            return rocks[rndEntity];
        else
            return grass[rndEntity];

    }
}
