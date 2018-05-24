using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public BuildingTemplate myBuilding;

    public bool isPlaced;
    public Material myMat;
    public bool inOtherBuilding;
    public bool startedPlacing;
    public LayerMask obstacles;
    Color normalColor;

    BoxCollider myCol;
    Vector3 sizeCol;

	public void Start ()
    {

        myMat = GetComponent<Renderer>().material;
        normalColor = myMat.color;
        myCol = GetComponentInChildren<BoxCollider>();
        sizeCol = new Vector3(myCol.size.x, myCol.size.z, myCol.size.y)/2;
        StartCoroutine(EnablePlacement());
    }

    IEnumerator EnablePlacement()
    {
        yield return new WaitForSeconds(0.01f);
        startedPlacing = true;       
    }
	
	void Update ()
    {
        if (!isPlaced)
        {
            CollisionStay();
        }
	}

    public void Place()
    {
        print(gameObject);
        print(myMat.color);
        myMat.color = normalColor;
        gameObject.layer = 8;
        isPlaced = true;
        AddStats ();
    }

    void AddStats () 
    {
        StatisticManager.instance.addWood += myBuilding.wood;
        StatisticManager.instance.addStone += myBuilding.stone;
        StatisticManager.instance.addMoney += myBuilding.money;
        StatisticManager.instance.addMinerals += myBuilding.minerals;
        StatisticManager.instance.addFood += myBuilding.food;
        StatisticManager.instance.addHappiness += myBuilding.happiness;
        StatisticManager.instance.addWater += myBuilding.water;
        StatisticManager.instance.addEnergy += myBuilding.energy;
        StatisticManager.instance.addCo2 += myBuilding.co2;
    }

    void MinStats () 
    {
        StatisticManager.instance.addWood -= myBuilding.wood;
        StatisticManager.instance.addStone -= myBuilding.stone;
        StatisticManager.instance.addMoney -= myBuilding.money;
        StatisticManager.instance.addMinerals -= myBuilding.minerals;
        StatisticManager.instance.addFood -= myBuilding.food;
        StatisticManager.instance.addHappiness -= myBuilding.happiness;
        StatisticManager.instance.addWater -= myBuilding.water;
        StatisticManager.instance.addEnergy -= myBuilding.energy;
        StatisticManager.instance.addCo2 -= myBuilding.co2;
    }

    void CollisionStay()
    {
        Collider[] buildings = Physics.OverlapBox(transform.position,sizeCol,Quaternion.identity,obstacles);
        if (buildings.Length != 0)
        {
            myMat.color = Color.red;
            inOtherBuilding = true;
        }
        else
        {
            myMat.color = Color.green;
            inOtherBuilding = false;
        }
    }

    public Vector3 GetColliderSize()
    {
        return sizeCol;
    }
}
