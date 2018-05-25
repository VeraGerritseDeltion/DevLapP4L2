using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraStats : MonoBehaviour {

    public LayerMask toCollideHouse;
    public float myRadius;
    public List<Collider> myHouses;

    [Header("Stats")]
    public int happyPoints;
    public int energyPoints;
    public int waterPoints;
    public int co2Points;

    public void AddList (Vector3 center)
    {

        Collider[] houses = Physics.OverlapSphere (center, myRadius, toCollideHouse);
        print (houses.Length);
        myHouses = new List<Collider> (houses);
    }
    
    public void AddStats ()
    {
        print ("execute");
        for(int j = 0; j < myHouses.Count; j++)
        {
            BuildingStats myBuilding = myHouses [j].GetComponent<BuildingStats> ();
            myBuilding.happiness += happyPoints;
            myBuilding.energy += energyPoints;
            myBuilding.water += waterPoints;
            myBuilding.co2 += co2Points;
        }
    }
}
