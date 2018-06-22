using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Camera myCamera;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        StartCoroutine(StartUp());
    }
    IEnumerator StartUp()
    {
        yield return new WaitForEndOfFrame();
        ManagerStartUp();
    }

    void ManagerStartUp()
    {
        if(UIManager.instance != null)
        {
            UIManager.instance.MyStart();
        }
        NatureManager.instance.MyStart();
        TreeInstantiationManager.instance.MyStart();
        LoadingScreenManager.instance.MyStart();
    }

    public void StartGame()
    {
        StatisticManager.instance.MyStart();
    }
}
