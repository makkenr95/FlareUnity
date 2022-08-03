using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationListHandler : MonoBehaviour
{
    public GameObject constellationList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateList() 
    {
        for(int i = 0; i < constellationList.transform.childCount; i++)
        {
            // Shuffle and show 3 itmes in list
            constellationList.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            int rnd = Random.Range(0, constellationList.transform.childCount);
            if (constellationList.transform.GetChild(rnd).gameObject.activeSelf)
            {
                i--;
                continue;
            }
            constellationList.transform.GetChild(rnd).gameObject.SetActive(true);
        }
    }
}
