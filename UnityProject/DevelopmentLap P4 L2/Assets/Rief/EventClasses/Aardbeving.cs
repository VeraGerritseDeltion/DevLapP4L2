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
        int beenHit = 0;
        List<GameObject> hitBuildings = new List<GameObject>();
        List<int> worksnow = new List<int>();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < BuildingManager.instance.allBuildings.Count; i++)
        {
            if(Random.Range(0,100) < percentage){
                BuildingManager.instance.allBuildings[i].GetComponent<Building>().dust.SetActive(true);
                worksnow.Add(i);
                hitBuildings.Add(BuildingManager.instance.allBuildings[i]);
            }
        }

        yield return new WaitForSeconds(timer);

        for(int j = 0; j < worksnow.Count; j++)
        {
            BuildingManager.instance.allBuildings[worksnow[j]].GetComponent<Building>().EventDestroy();

            beenHit++;
        }
        for (int i = 0; i < hitBuildings.Count; i++)
        {
            BuildingManager.instance.allBuildings.Remove(hitBuildings[i]);
        }
        GameManager.instance.myCamera.GetComponent<CameraShake>().shouldShake = false;
        UIManager.instance.EventLog(beenHit.ToString() + " buildings destroyed.");
    }
}
