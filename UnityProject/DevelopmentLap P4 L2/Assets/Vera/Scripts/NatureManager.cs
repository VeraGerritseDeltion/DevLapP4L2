﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureManager : MonoBehaviour
{
    public static NatureManager instance;

    public float uitstoot;

    [Header("Start Colors nature")]
    public Color currentHigh;
    public Color currentLow;

    public Color startColorHigh;
    public Color startColorLow;
    [Header("Mid Colors nature")]
    public Color midColorHigh;
    public Color midColorLow;
    [Header("End Colors nature")]
    public Color endColorHigh;
    public Color endColorLow;

    [Header("Vegetation")]
    public List<GameObject> allTrees = new List<GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void MyStart () {
        CalculateProcent();
	}

    public void CalculateProcent()
    {
        bool LowerOrHigher = false;
        float procent = 0;
        if (uitstoot > 50)
        {
            float mid = uitstoot - 50;
            procent = mid / 50;
            LowerOrHigher = true;
        }
        else
        {
            procent = uitstoot/ 50;
        }
        ChangeNature(LowerOrHigher, procent);
    }

    void ChangeNature(bool lower, float procent)
    {
        if (!lower)
        {
            currentHigh = Color.Lerp(startColorHigh, midColorHigh, procent);
            currentLow = Color.Lerp(startColorLow, midColorLow, procent);
        }
        else
        {
            currentHigh = Color.Lerp(midColorHigh, endColorHigh, procent);
            currentLow = Color.Lerp(midColorLow, endColorLow, procent);
        }
        for (int i = 0; i < allTrees.Count; i++)
        {
            float rand = allTrees[i].GetComponent<Trees>().myRand;
            Color newColor = Color.Lerp(currentHigh, currentLow, rand);
            allTrees[i].GetComponent<Trees>().ChangeColor(newColor);
        }
    }

    void UpdatesTrees()
    {

    }

    IEnumerator UpdateTreesFast()
    {
        yield return new WaitForSeconds(0.1f);
        uitstoot += 1;
        CalculateProcent();
        StartCoroutine(UpdateTreesFast());
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(UpdateTreesFast());
        }
	}
}
