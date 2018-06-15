using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aardbeving : Events{

    int percentage;


    void Start()
    {

    }

    void Update()
    {

    }

   	public override void Occur()
    {
        for (int i = 0; i < BuildingManager.instance.allBuildings.Count; i++)
        {
            if(Random.Range(0,100) < percentage){
                //animatie
                //BuildingManager.instance.allBuildings[i].
            }
        }
    }
}
