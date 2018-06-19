using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Building : MonoBehaviour{

    public BuildingTemplate myBuilding;

    public bool isPlaced;
    public Material myMat;
    public bool inOtherBuilding;
    public bool startedPlacing;
    public LayerMask obstacles;
    Color normalColor;

    BoxCollider myCol;
    Vector3 sizeCol;

    public int ageLock;

    public int woodCost;
    public int stoneCost;
    public int moneyCost;
    bool purchaseAble;


    public bool hasAura;

    private BuildingStats myBuildingStats;



    //Tooltip Stuff
    public GameObject tooltip;
    public TextMeshProUGUI myName;

    public List<string> myStrings;
    public List<TextMeshProUGUI> myText;

    void Start()
    {
        TextTooltip();
        if(myName != null)
        {
            myName.text = myBuilding.name;
        }
    }

    public List<int> VarList()
    {
        List<int> myVarList = new List<int>();
        if(myBuilding!= null)
        {
            myVarList.Add(myBuilding.money);
            myVarList.Add(myBuilding.wood);
            myVarList.Add(myBuilding.stone);
            myVarList.Add(myBuilding.food);
        }
        return myVarList;
    }

    void TextTooltip()
    {
        List<int> amounts = VarList();
        int checkZero = 0;
        for (int i = 0; i < myText.Count; i++)
        {
            for (int o = 0; checkZero < amounts.Count; checkZero++)
            {
                o++;
                if (amounts[checkZero] != 0)
                {
                    myText[i].enabled = true;
                    myText[i].text = myStrings[checkZero] + amounts[checkZero];
                    checkZero++;
                    break;
                }
            }
            if (checkZero > amounts.Count)
            {
                myText[i].enabled = false;
            }
        }
    }

    public void MyStart()
    {
        myBuildingStats = transform.GetComponent<BuildingStats>();
        Renderer myRend = GetComponent<Renderer>();
        if (myRend == null)
        {
            myRend = GetComponentInChildren<Renderer>();
        }
        myMat = myRend.material;
        if (myMat == null)
        {
            isPlaced = true;
        }
        normalColor = myMat.color;
        myCol = GetComponentInChildren<BoxCollider>();
        if (myRend == null)
        {
            myCol = GetComponent<BoxCollider>();
        }

        sizeCol = new Vector3(myCol.size.x, myCol.size.z, myCol.size.y) / 2;
        StartCoroutine(EnablePlacement());
    }

    IEnumerator EnablePlacement()
    {
        yield return new WaitForSeconds(0.01f);
        startedPlacing = true;
    }

    void Update()
    {
        if (!isPlaced)
        {
            CollisionStay();
        }
    }

    public void Place()
    {
        if (GetType() != typeof(TownHall))
        {
            BuildingManager.instance.allBuildings.Add(gameObject);
        }
        myMat.color = normalColor;
        gameObject.layer = 8;
        if (myBuilding != null)
        {
            AddStats();
            GetComponent<BuildingStats>().AddToAura();
        }
        isPlaced = true;
        StatisticManager.instance.wood -= woodCost;
        StatisticManager.instance.stone -= stoneCost;
        StatisticManager.instance.money -= moneyCost;
    }

    public void EventDestroy()
    {
        StartCoroutine(GoDown());
    }

    IEnumerator GoDown()
    {
        float time = 0;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.05f);
            time += 0.1f;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z), 0.1f);
        }
        yield return new WaitForSeconds(1);
        DestroyBuilding();
    }

    public void DestroyBuilding()
    {
        if(GetType() == typeof(TownHall))
        {
            BuildingManager.instance.myTownHall = null;
            BuildingManager.instance.bp.townhallPlaced = false; 
        }
        else
        {
            BuildingManager.instance.allBuildings.Remove(gameObject);
        }
        MinStats();
        Destroy(gameObject);
    }

    void AddStats()
    {
        StatisticManager myStatisticManager = StatisticManager.instance;

        if (myStatisticManager.wood < myStatisticManager.woodStorage)
        {
            myStatisticManager.addWood += myBuilding.wood;
        }
        if (myStatisticManager.stone < myStatisticManager.stoneStorage)
        {
            myStatisticManager.addStone += myBuilding.stone;
        }
        if (myStatisticManager.money < myStatisticManager.moneyStorage)
        {
            myStatisticManager.addMoney += myBuilding.money;
        }
        if (myStatisticManager.food < myStatisticManager.foodStorage)
        {
            myStatisticManager.addFood += myBuilding.food;
        }
        myStatisticManager.addMinerals += myBuilding.minerals;

        myStatisticManager.woodStorage += myBuilding.woodStorage;
        myStatisticManager.stoneStorage += myBuilding.stoneStorage;
        myStatisticManager.moneyStorage += myBuilding.moneyStorage;
        myStatisticManager.foodStorage += myBuilding.foodStorage;
    }

    void MinStats()
    {
        if(myBuilding != null)
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
    }

    void CollisionStay()
    {
        //print(isPlaced);
        float offSet = 0.05f;
        Vector3 size = new Vector3(sizeCol.x - offSet, sizeCol.y - offSet, sizeCol.z - offSet);
        Collider[] buildings = Physics.OverlapBox(transform.position, size, Quaternion.identity, obstacles);
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
        if (!active)
        {
            myMat.color = normalColor;
        }
        if (active)
        {
            myMat.color = Color.yellow;
        }
    }

    public Vector3 GetColliderSize()
    {
        return sizeCol;
    }

    public bool canPurchase()
    {
        if (ageLock < StatisticManager.instance.age && StatisticManager.instance.wood >= woodCost && StatisticManager.instance.stone >= stoneCost && StatisticManager.instance.money >= moneyCost)
        {
            purchaseAble = true;
        }
        else
        {
            purchaseAble = false;
        }
        return purchaseAble;
    }
    public void Upgrade()
    {
        //upgrades building
    }
    public void Tooltip(bool active)
    {
        tooltip.SetActive(active);
    }
}
