using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public int level = 0;
    public virtual void ApplyEffect()
    {
        level++;
        GameObject.FindGameObjectWithTag("ConstellationHUD").SetActive(false);
        Time.timeScale = 1;
    }
}
