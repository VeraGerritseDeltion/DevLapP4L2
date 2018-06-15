using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aardbeving : Events{

    int percentage;

   	public override void Occur()
    {
        for (int i = 0; i < BuildingManager.instance.allBuildings.Count; i++)
        {
            if(Random.Range(0,100) < percentage){
                //particle
                BuildingManager.instance.allBuildings.Remove(BuildingManager.instance.allBuildings[i]);
            }
        }
    }
}
