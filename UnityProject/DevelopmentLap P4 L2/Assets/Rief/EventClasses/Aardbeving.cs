using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aardbeving : Events{

    public int percentage;
    public int timer;


    public override void Occur()
    {
        base.Occur();
        GameManager.instance.myCamera.GetComponent<CameraShake>().shouldShake = true;
        StartCoroutine(Happening());

    }
    IEnumerator Happening()
    {
        yield return new WaitForSeconds(timer);
        int beenHit = 0;
        for (int i = 0; i < BuildingManager.instance.allBuildings.Count; i++)
        {
            if(Random.Range(0,100) < percentage){
                //particle
                //BuildingManager.instance.allBuildings.Remove(BuildingManager.instance.allBuildings[i]);
                BuildingManager.instance.allBuildings[i].GetComponent<Building>().EventDestroy();
                beenHit++;
            }
        }
        GameManager.instance.myCamera.GetComponent<CameraShake>().shouldShake = false;
        UIManager.instance.EventLog(beenHit.ToString() + " buildings destroyed.");
    }
}
