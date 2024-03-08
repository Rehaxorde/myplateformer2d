using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public int maxMagic = 5;
    public int currentMagic;

    public MPBar mpBar;
    public GameObject player;

    void Start()
    {
        currentMagic = maxMagic;
        mpBar.SetMaxMagic(maxMagic);
    }

    void Update()
    {

    }

    public void MagicCost(int point)
    {
            currentMagic -= point;
            mpBar.SetMagic(currentMagic);       
    }
    public void MagicGain(int mana)
    {
        currentMagic += mana;
        mpBar.SetMagic(currentMagic);
    }

    public int GetCurrentMagic()
    {
        return currentMagic;
    }
    public int GetMagicGain()
    {
        return currentMagic;
    }
}

