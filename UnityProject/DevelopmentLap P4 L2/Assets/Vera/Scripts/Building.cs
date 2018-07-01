using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Building : MonoBehaviour{

    public BuildingTemplate myBuilding;
    public GameObject upgradeBuild;
    public bool needsWorkers;
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
    public int citizenCost;
    bool purchaseAble;

    public bool hasAura;

    private BuildingStats myBuildingStats;

    public float radius;

    //Tooltip Stuff
    public GameObject tooltip;
    public TextMeshProUGUI myName;

    public List<string> myStrings;
    public List<TextMeshProUGUI> myText;
    public GameObject dust;

    public List<int> myCitizens;

    int spotAvailable;

    public bool clickToStart;

    void Start()
    {
        TextTooltip();
        if(myName != null)
        {
            myName.text = myBuilding.name;
        }
        StartCoroutine(EnablePlacement());
    }

    public virtual void LumberAndCrops()
    {
        
    }

    public List<int> VarList()
    {
        List<int> myVarList = new List<int>();
        if (myBuilding != null)
        {
            if (GetType() == typeof(TownHall))
            {

            }
            else
            {
                myVarList.Add(myBuilding.money);
                myVarList.Add(myBuilding.wood);
                myVarList.Add(myBuilding.stone);
                myVarList.Add(myBuilding.food);
                myVarList.Add(myBuilding.co2);
                myVarList.Add(myBuilding.moneyStorage);
                myVarList.Add(myBuilding.woodStorage);
                myVarList.Add(myBuilding.stoneStorage);
                myVarList.Add(myBuilding.foodStorage);
            }
        }
        return myVarList;
    }

    void AddCitizens()
    {
        StatisticManager.instance.allCitizens += myCitizens.Count;
        StatisticManager.instance.citizens += myCitizens.Count;
        for (int i = 0; i < myCitizens.Count; i++)
        {
            StatisticManager.instance.happiness += myCitizens[i];
        }
    }
    void MinusHappiness()
    {
        int happyMin = 10;
        StatisticManager.instance.happiness -= (myCitizens.Count * happyMin);
        for (int i = 0; i < myCitizens.Count; i++)
        {
            myCitizens[i] -= happyMin;
            StatisticManager.instance.homeless.Add(myCitizens[i]);
        }
    }
    void TextTooltip()
    {
        if (GetType() == typeof(TownHall))
        {
            TownHallText();
        }
        else
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
    }
    void TownHallText()
    {
         myText[0].text = "Citizens: " + StatisticManager.instance.allCitizens
                            + "\nAverage Happiness: " + StatisticManager.instance.avrHappiness
                            + "\nWood Increase /s: " + StatisticManager.instance.addWood
                            + "\nStone Increase /s: " + StatisticManager.instance.addStone
                            + "\nFood Increase /s: " + StatisticManager.instance.addFood;
    }

    public void MyStart()
    {
        spotAvailable = myBuilding.citizens;
        //myBuildingStats = transform.GetComponent<BuildingStats>();
        radius = 10;
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

        sizeCol = new Vector3(myCol.size.x, myCol.size.y, myCol.size.z) / 2;

    }

    IEnumerator EnablePlacement()
    {
        yield return new WaitForSeconds(0.01f);
        startedPlacing = true;
    }

    void Update()
    {
        if(GetType() == typeof(TownHall))
        {
            TownHallText();
        }
        
        if (!isPlaced)
        {
            CollisionStay();
        }
    }

    public void Place()
    {
        StatisticManager.instance.wood -= woodCost;
        StatisticManager.instance.stone -= stoneCost;
        StatisticManager.instance.money -= moneyCost;
        StatisticManager.instance.citizens -= citizenCost;
        CitizenCheck();
        LumberAndCrops();
        if (GetType() == typeof(TownHall) || GetType() == typeof(Road) || GetType() == typeof(Storage))
        {

        }
        else
        {
            BuildingManager.instance.allBuildings.Add(gameObject);
            AddCitizens();
        }

        myMat.color = normalColor;
        gameObject.layer = 8;
        if (myBuilding != null)
        {
            AddStats();
            if (GetComponent<BuildingStats>())
            {
                GetComponent<BuildingStats>().AddToAura();
            }
        }
        isPlaced = true;
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
        MinusHappiness();
        Destroy(gameObject);     
    }

    public void AddStats()
    {
        StatisticManager myStatisticManager = StatisticManager.instance;

        if(!clickToStart){
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
            StatisticManager.instance.addCo2 += myBuilding.co2;
            StatisticManager.instance.AverageHappiness();
        }
    }

    public void MinStats()
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
            StatisticManager.instance.addCo2 -= myBuilding.co2;
            StatisticManager.instance.AverageHappiness();
        }
    }

    void CollisionStay()
    {
        //print(isPlaced);
        float offSet = 0.05f;
        Vector3 size = new Vector3(sizeCol.x - offSet, sizeCol.y - offSet, sizeCol.z - offSet);
        Collider[] buildingsCloseBy = Physics.OverlapSphere(transform.position, radius, BuildingManager.instance.bp.buildingLayer);
        Collider[] buildings = Physics.OverlapBox(transform.position, size, Quaternion.identity, obstacles);
        if (buildingsCloseBy.Length == 0 && this.GetType() != typeof(TownHall)) 
        {
            myMat.color = Color.red;
            inOtherBuilding = true;
        }
        else if (buildings.Length != 0 || !canPurchase())
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

    public void TurnCol()
    {
        float oldX = sizeCol.x;
        float oldZ = sizeCol.z;

        sizeCol.x = oldZ;
        sizeCol.z = oldX;
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
        if (ageLock < StatisticManager.instance.age && StatisticManager.instance.wood >= woodCost && StatisticManager.instance.stone >= stoneCost && StatisticManager.instance.money >= moneyCost && StatisticManager.instance.citizens >= citizenCost)
        {
            purchaseAble = true;
        }
        else
        {
            purchaseAble = false;
        }
        return purchaseAble;
    }
    public void UpgradeHouse()
    {
        GameObject spawnedUpgrade =  Instantiate(upgradeBuild, transform.position, transform.rotation);
        MinStats();
        spawnedUpgrade.GetComponent<Building>().MyStart();
        spawnedUpgrade.GetComponent<Building>().AddStats();
        spawnedUpgrade.GetComponent<Building>().myCitizens = myCitizens;
        spawnedUpgrade.GetComponent<Building>().CitizenCheck();
        Destroy(this.gameObject);
    }

    public void CitizenCheck()
    {
        spotAvailable = myBuilding.citizens;
        while(StatisticManager.instance.homeless.Count > 0 && myCitizens.Count < spotAvailable)
        {
            StatisticManager.instance.allCitizens--;
            StatisticManager.instance.citizens--;
            myCitizens.Add(StatisticManager.instance.homeless[0]);
            StatisticManager.instance.homeless.RemoveAt(0);
        }
        while(myCitizens.Count < spotAvailable)
        {
            myCitizens.Add(StatisticManager.instance.startHappiness);
        }
    }
    public void Tooltip(bool active)
    {
        Vector3 pos = transform.position;

        //float ttHeight = 800/0.03f;
        //ttHeight = ttHeight/2;
        //print(pos);

        pos.y += sizeCol.y + 12f;

        //pos.x = Mathf.Abs(pos.x);
        //tooltip.transform.position = pos;
        tooltip.SetActive(active);
    }
}
