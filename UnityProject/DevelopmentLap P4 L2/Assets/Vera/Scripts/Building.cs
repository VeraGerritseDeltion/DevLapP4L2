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

    public int woodCost;
    public int stoneCost;
    public int moneyCost;
    bool purchaseAble;


    public bool hasAura;

    private BuildingStats myBuildingStats;

    public void MyStart ()
    {
        myBuildingStats = transform.GetComponent<BuildingStats> ();
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
        myMat.color = normalColor;
        gameObject.layer = 8;
        if(myBuilding != null)
        {
            AddStats();
            GetComponent<BuildingStats>().AddToAura();

        }
        isPlaced = true;
        StatisticManager.instance.wood -= woodCost;
        StatisticManager.instance.stone -= stoneCost;
        StatisticManager.instance.money -= moneyCost;
    }
    
    public void DestroyBuilding()
    {
        MinStats();
        Destroy(gameObject);
    }


    void AddStats () 
    {
        StatisticManager.instance.addWood += myBuilding.wood;
        StatisticManager.instance.addStone += myBuilding.stone;
        StatisticManager.instance.addMoney += myBuilding.money;
        StatisticManager.instance.addMinerals += myBuilding.minerals;
        StatisticManager.instance.addFood += myBuilding.food;
    }

    void MinStats () 
    {
        StatisticManager.instance.addWood -= myBuilding.wood;
        StatisticManager.instance.addStone -= myBuilding.stone;
        StatisticManager.instance.addMoney -= myBuilding.money;
        StatisticManager.instance.addMinerals -= myBuilding.minerals;
        StatisticManager.instance.addFood -= myBuilding.food;
    }

    void CollisionStay()
    {
        float offSet = 0.05f;
        Vector3 size = new Vector3(sizeCol.x - offSet, sizeCol.y - offSet, sizeCol.z - offSet);
        Collider[] buildings = Physics.OverlapBox(transform.position,size,Quaternion.identity,obstacles);
        if (buildings.Length != 0 || !canPurchase())
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

    public void HighlightBuilding()
    {
        myMat.color = Color.yellow;
    }

    public void DeHighlightBuilding()
    {
        myMat.color = normalColor;
    }

    public Vector3 GetColliderSize()
    {
        return sizeCol;
    }

    public bool canPurchase(){
        if(StatisticManager.instance.wood >= woodCost && StatisticManager.instance.stone >= stoneCost && StatisticManager.instance.money >= moneyCost)
        {
            purchaseAble = true;
        }
        else
        {
            purchaseAble = false;
        }
        return purchaseAble;
    }
}
