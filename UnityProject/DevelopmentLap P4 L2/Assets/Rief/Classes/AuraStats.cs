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


    void Start(){
        AddList();
    }
    public void AddList ()
    {
        Collider[] houses = Physics.OverlapSphere (transform.position, myRadius, toCollideHouse);
        myHouses = new List<Collider> (houses);

        AddStats();
    }
    
    public void AddStats ()
    {
        for(int j = 0; j < myHouses.Count; j++)
        {
            BuildingStats myBuilding = myHouses [j].GetComponent<BuildingStats> ();
            myBuilding.happiness += happyPoints;
            myBuilding.energy += energyPoints;
            myBuilding.water += waterPoints;
            myBuilding.co2 -= co2Points;
        }
    }
void RemoveAura(){
    for (int i = 0; i < myHouses.Count; i++){
        BuildingStats myBuilding = myHouses[i].GetComponent<BuildingStats>();
        myBuilding.happiness -= happyPoints;
        myBuilding.energy -= energyPoints;
        myBuilding.water -= waterPoints;
        myBuilding.co2 += co2Points;
        myHouses.Remove(myHouses[i]);
    }
}
}
