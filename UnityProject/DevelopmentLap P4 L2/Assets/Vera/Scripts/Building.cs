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


    //Tooltip Stuff
    public GameObject tooltip;

    public void MyStart ()
    {
        myBuildingStats = transform.GetComponent<BuildingStats> ();
        Renderer myRend = GetComponent<Renderer>();
        if(myRend == null)
        {
            myRend = GetComponentInChildren<Renderer>();
        }
        myMat = myRend.material;
        print(myMat);
        if(myMat == null)
        {
            isPlaced = true;
        }
        normalColor = myMat.color;
        myCol = GetComponentInChildren<BoxCollider>();
        if (myRend == null)
        {
            myCol = GetComponent<BoxCollider>();
        }
        sizeCol = new Vector3(myCol.size.x, myCol.size.z, myCol.size.y)/2;
        print("Start");
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
        StatisticManager myStatisticManager = StatisticManager.instance;

        if(myStatisticManager.wood < myStatisticManager.woodStorage){
            myStatisticManager.addWood += myBuilding.wood;
        }
        if(myStatisticManager.stone < myStatisticManager.stoneStorage){  
            myStatisticManager.addStone += myBuilding.stone;
        }
        if(myStatisticManager.money < myStatisticManager.moneyStorage){
            myStatisticManager.addMoney += myBuilding.money;
        }
        if(myStatisticManager.food < myStatisticManager.foodStorage){
            myStatisticManager.addFood += myBuilding.food;
        }
        myStatisticManager.addMinerals += myBuilding.minerals;

        myStatisticManager.woodStorage += myBuilding.woodStorage;
        myStatisticManager.stoneStorage += myBuilding.stoneStorage;
        myStatisticManager.moneyStorage += myBuilding.moneyStorage;
        myStatisticManager.foodStorage += myBuilding.foodStorage;
    }

    void MinStats () 
    {
        StatisticManager.instance.addWood -= myBuilding.wood;
        StatisticManager.instance.addStone -= myBuilding.stone;
        StatisticManager.instance.addMoney -= myBuilding.money;
        StatisticManager.instance.addMinerals -= myBuilding.minerals;
        StatisticManager.instance.addFood -= myBuilding.food;

        StatisticManager.instance.woodStorage -= myBuilding.woodStorage;
        StatisticManager.instance.stoneStorage -= myBuilding.stoneStorage;
        StatisticManager.instance.moneyStorage -= myBuilding.moneyStorage;
        StatisticManager.instance.foodStorage -= myBuilding.foodStorage;
        
    }

    void CollisionStay()
    {
        print(isPlaced);
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

    public void HighlightBuilding(bool active)
    {
        if(!active)
        {
            myMat.color = normalColor;
        }
        if(active)
        {
            myMat.color = Color.yellow;
        }
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
    public void Upgrade () 
    {
        //upgrades building
    }
    public void Tooltip (bool active)
    {
        tooltip.SetActive(active);
    }
}
