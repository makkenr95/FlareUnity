using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class TilingGridScript : MonoBehaviour
{
    //public Material material;
    public Sprite whiteSprite;
    public Color color;
    GameObject line;
    GameObject player;
    List<GameObject> linesY = new List<GameObject>();
    List<GameObject> linesX = new List<GameObject>();

    public float lineThickness = 0.03f;
    public float lineLength = 100f;

    public float spacing = 1;
    public int amount = 20;

    public bool reStart = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.position = player.transform.position;

        line = new GameObject();
        var sR = line.AddComponent<SpriteRenderer>();
        //sR.material = material;
        sR.color = color;
        sR.sprite = whiteSprite;
        sR.sortingLayerName = "Background";
        sR.sortingOrder = 1;
        line.transform.localScale = new Vector3(100, 0.03f);
        var offset = (spacing * amount) / 2;
        for (int y = 0; y < amount; y++)
        {
            linesY.Add(Instantiate<GameObject>(line, new Vector3(0, offset - spacing * y, 1), new Quaternion(0, 0, 0, 0), gameObject.transform));
        }
        line.transform.localScale = new Vector3(0.03f, 100);
        for (int x = 0; x < amount; x++)
        {
            linesX.Add(Instantiate<GameObject>(line, new Vector3(offset - spacing * x, 0, 1), new Quaternion(0, 0, 0, 0), gameObject.transform));
        }
        DestroyImmediate(line);
    }

    // Update is called once per frame
    void Update()
    {
        if (reStart)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
            Start();
            reStart = false;
        }
        if ((player.transform.position.y - gameObject.transform.position.y) > spacing)
        {
            gameObject.transform.Translate(0, spacing, 0);
        }
        else if ((player.transform.position.y - gameObject.transform.position.y) < -spacing)
        {
            gameObject.transform.Translate(0, -spacing, 0);
        }
        if ((player.transform.position.x - gameObject.transform.position.x) > spacing)
        {
            gameObject.transform.Translate(spacing, 0, 0);
        }
        else if ((player.transform.position.x - gameObject.transform.position.x) < -spacing)
        {
            gameObject.transform.Translate(-spacing, 0, 0);
        }
    }
}
