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

    private Collider [] myAuras;

    public void Start ()
    {
        //myAuras = AuraEffect ();
    }

    public Collider [] AuraEffect ()
    {
        Collider [] auras = Physics.OverlapSphere (transform.position, myRadius, aura);

        for (int i = 0; i < auras.Length; i++)
        {
            AuraStats myAura = auras [i].GetComponent<AuraStats> ();
            myAura.myHouses.Add (gameObject.GetComponent<Collider>());
        }
        return auras;
    }

    void ToDestroy ()
    {
        for(int i = 0; i < myAuras.Length; i++)
        {
            AuraStats myAura = myAuras [i].GetComponent<AuraStats> ();
            myAura.myHouses.Remove (gameObject.GetComponent<Collider>());
        }
    }
}