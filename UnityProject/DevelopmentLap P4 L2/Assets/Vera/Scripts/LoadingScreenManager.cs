﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour {
    public static LoadingScreenManager instance;
    public Canvas loadingScreen;
    public float currentStatus;
    float maxStatus;

    public Image bar;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        loadingScreen.enabled = true;
    }

    public void MyStart()
    {
        maxStatus = TreeInstantiationManager.instance.treeLoc.Count;
        print(maxStatus + " max");
        currentStatus = -1;
        UpdateMe();
    }

    public void UpdateMe()
    {
        currentStatus++;
        float procent = currentStatus / maxStatus;
        bar.fillAmount = procent;
    }

    public void Done()
    {
        loadingScreen.enabled = false;
        GameManager.instance.StartGame();
    }
}