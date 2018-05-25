using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraStats : MonoBehaviour {

    public LayerMask toCollideHouse;
    public float myRadius;
    public List<GameObject> myHouses = new List<GameObject>();

    [Header("Stats")]
    public int happyPoints;
    public int energyPoints;
    public int waterPoints;
    public int co2Points;


    public void AddList (Vector3 center) {

        Collider [] houses = Physics.OverlapSphere (center, myRadius, toCollideHouse);

        for(int i = 0; i < houses.Length; i++) {
            myHouses.Add (houses [i].gameObject);
        }
    }
    
    public void AddStats () {
        
        for(int j = 0; j < myHouses.Count; j++) {
            BuildingStats myBuilding = myHouses [j].GetComponent<BuildingStats> ();
            myBuilding.happiness += happyPoints;
            myBuilding.energy += energyPoints;
            myBuilding.water += waterPoints;
            myBuilding.co2 += co2Points;
        }
    }
}
