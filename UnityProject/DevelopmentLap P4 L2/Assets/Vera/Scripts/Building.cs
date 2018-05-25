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


    public bool hasAura;

    private BuildingStats myBuildingStats;

    void Start ()
    {
        Place ();
    }
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
        //gameObject.layer = 8;
        isPlaced = true;
        if(myBuilding != null)
        {
            AddStats();

            if (hasAura == true)
            {
                GetComponent<AuraStats> ().AddList (transform.position);
                GetComponent<AuraStats> ().AddStats ();
            }
        }
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
