using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        StatisticManager.instance.MyStart();
    }
}
