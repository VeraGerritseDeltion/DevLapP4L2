using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStats : MonoBehaviour {

    public LayerMask aura;
    public float myRadius;

    [Header("Required stats")]
    public int reqHappiness;
    public int reqWater;
    public int reqEnergy;
    public int reqCo2;

    [Header("Current Stats")]
    public int happiness;
    public int water;
    public int energy;
    public int co2;
    

public void AddToAura(){
    Collider[] auras = Physics.OverlapSphere(transform.position, myRadius, aura);

    for (int i = 0; i < auras.Length; i++){
        AuraStats auraStats= auras[i].GetComponent<AuraStats>();
        auraStats.myHouses.Add(gameObject.GetComponent<Collider>());
        happiness += auraStats.happyPoints;
        water += auraStats.waterPoints;
        energy += auraStats.energyPoints;
        co2 += auraStats.co2Points;
    }
}
}