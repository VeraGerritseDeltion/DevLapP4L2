using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour {
    public static LoadingScreenManager instance;
    public RectTransform loadingScreen;
    public float currentStatus;
    float maxStatus;

    public Image bar;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        loadingScreen.gameObject.SetActive(true);
    }

    public void MyStart()
    {
        maxStatus = TreeInstantiationManager.instance.treeLoc.Count;
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
        loadingScreen.gameObject.SetActive(false);
        UIManager.instance.loading = false;
        GameManager.instance.StartGame();
    }
}
