using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlacement : MonoBehaviour {

    [Header("testVar")]
    //public GameObject testBuilding;
    public LayerMask obstacleLayer;
    public LayerMask groundLayer;
    bool startedPlacing;

    GameObject isPlacing;
    Building placing;
    int index;
    public List<GameObject> allBuildings = new List<GameObject>();

    [Header ("Road Var")]
    List<GameObject> allRoadsToPlace = new List<GameObject>();
    bool placingRoad;
    bool road;
    Vector3 startRoad;
    GameObject roadToBePlaced;
    public LineRenderer line;
    Material lineMat;
    bool notFirstFrame;
    bool continuePlacing;

    public int ageLock;

    void Start () {
        lineMat = line.material;
	}
	

	void Update () {
        if (startedPlacing)
        {
            PlaceBuilding(null,index,placingRoad);
        }
        if (road)
        {
            PlaceRoad(roadToBePlaced, index, startRoad);
        }
	}

    void PlaceBuilding(GameObject toBePlaced,int inndex,bool myRoad)
    {
        index = inndex;
        if (Input.GetButtonDown("Fire2"))
        {
            Destroy(isPlacing);
            isPlacing = null;
            startedPlacing = false;
            placing = null;
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 mouse3DPos = new Vector3(0,0,0);
        if (Physics.Raycast(ray, out hit,1000,groundLayer))
        {
            mouse3DPos = hit.point;
            if (isPlacing != null)
            {
                isPlacing.SetActive(true);
            }
        }
        else
        {
            if(isPlacing != null)
            {
                isPlacing.SetActive(false);
            }
        }

        if (!startedPlacing)
        {
            startedPlacing = true;
            isPlacing = Instantiate(toBePlaced, mouse3DPos, Quaternion.identity);
            placing = isPlacing.GetComponent<Building>();
            placing = isPlacing.GetComponentInChildren<Building>();
            placing.MyStart();
            if (placing == null)
            {              
                placing = isPlacing.GetComponentInChildren<Building>();
            }
            //placing.MyStart();
            placing.obstacles = obstacleLayer;
        }
        
        if(isPlacing != null && startedPlacing)
        {
            if (Input.GetButton("LeftShift") || Input.GetButton("LeftControl"))
            {
                print("test");  
                SnapTo(mouse3DPos);
            }
            else
            {
                isPlacing.transform.position = mouse3DPos;
            }
            if(Input.GetButtonDown("Fire1") && !placing.inOtherBuilding && placing.startedPlacing)
            {
                startedPlacing = false;

                if (myRoad)
                {
                    startRoad = isPlacing.transform.position;
                    roadToBePlaced = allBuildings[index];
                    road = true;
                    allRoadsToPlace.Add(isPlacing);
                    isPlacing = null;
                    placing = null;
                    return;
                }
                placing.Place();
                isPlacing = null;
                placing = null;
                PlaceBuilding(allBuildings[inndex],inndex,placingRoad);
            }
        }
    }


    void SnapTo(Vector3 mousePos)
    {
        Vector3 sizeCol = placing.GetColliderSize();
        Vector3 sizeSearchBox = new Vector3(sizeCol.x + 0.5f, sizeCol.y, sizeCol.z + 0.5f);

        Collider[] inRange = Physics.OverlapBox(mousePos, sizeSearchBox, Quaternion.identity, obstacleLayer);
        GameObject closest = null;
        print(inRange.Length);
        if(inRange.Length != 0)
        {
            float lowestRange = 0;
            int lowest = -1;
            for (int i = 0; i < inRange.Length; i++)
            {
                if(Vector3.Distance(mousePos,inRange[i].gameObject.transform.position) < lowestRange || lowestRange == 0)
                {
                    lowestRange = Vector3.Distance(mousePos, inRange[i].gameObject.transform.position);
                    lowest = i;
                }
            }
            closest = inRange[lowest].gameObject;
            Building closeBuild = closest.GetComponentInChildren<Building>();
            if (closeBuild == null)
            {
                closeBuild = GetComponent<Building>();
            }
            if (closeBuild == null)
            {
                isPlacing.transform.position = mousePos;
                return;
            }
            Vector3 closeColSize = closeBuild.GetColliderSize();
            Vector3 posClosest = closest.transform.position;
            float disX = posClosest.x - mousePos.x;
            float disZ = posClosest.z - mousePos.z;
            Vector3 snappedPos = new Vector3(0, 0, 0);
            float disx = Mathf.Abs(disX);
            float disz = Mathf.Abs(disZ);
            bool trueSnap = false;
            if (Input.GetButton("LeftControl"))
            {
                trueSnap = true;
            }
            if (disx > disz)
            {
                if(disX > 0)
                {
                    snappedPos = new Vector3(posClosest.x - sizeCol.x - closeColSize.x, mousePos.y, mousePos.z);
                    if (trueSnap)
                    {
                        snappedPos.z = posClosest.z;
                    }
                }
                else
                {
                    snappedPos = new Vector3(posClosest.x + sizeCol.x + closeColSize.x, mousePos.y, mousePos.z);
                    if (trueSnap)
                    {
                        snappedPos.z = posClosest.z;
                    }
                }
            }
            else
            {
                if(disZ > 0)
                {
                    snappedPos = new Vector3(mousePos.x, mousePos.y, posClosest.z - sizeCol.z - closeColSize.z);
                    if (trueSnap)
                    {
                        snappedPos.x = posClosest.x;
                    }
                }
                else
                {
                    snappedPos = new Vector3(mousePos.x, mousePos.y, posClosest.z + sizeCol.z + closeColSize.z);
                    if (trueSnap)
                    {
                        snappedPos.x = posClosest.x;
                    }
                }
            }
            isPlacing.transform.position = snappedPos;
        }
        else
        {
            isPlacing.transform.position = mousePos;
        }
    }

    public void NewBuilding(int whichBuilding)
    {
        if(ageLock >= StatisticManager.instance.age){
            bool newRoad = false;
            Building newBuilding = allBuildings[whichBuilding].GetComponent<Building>();
            newBuilding = allBuildings[whichBuilding].GetComponentInChildren<Building>();
            if(newBuilding.GetType() == typeof(Road))
            {
                newRoad = true;
            }
            placingRoad = newRoad;
            if(whichBuilding < allBuildings.Count)
            {
                PlaceBuilding(allBuildings[whichBuilding],whichBuilding,newRoad);
            }
        }
    }

    void PlaceRoad(GameObject toBePlaced, int inndex, Vector3 startPos)
    {
        line.enabled = true;
        bool obstructed = false;
        BoxCollider myRoad = toBePlaced.GetComponent<BoxCollider>();
        if(myRoad == null)
        {
            myRoad = toBePlaced.GetComponent<BoxCollider>();
        }
        Vector3 colSize = new Vector3(myRoad.size.x,myRoad.size.y,myRoad.size.z);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 mouse3DPos = new Vector3(0, 0, 0);

        bool vertPos = false;
        bool hor = false;
        bool horPos = false;
        int amountBlocks = 0;

        if (Physics.Raycast(ray, out hit, 1000, groundLayer))
        {
            mouse3DPos = hit.point;
        }

        float disX = startPos.x - mouse3DPos.x;
        float disZ = startPos.z - mouse3DPos.z;
        Vector3 snappedPos = new Vector3(0, 0, 0);
        float disx = Mathf.Abs(disX);
        float disz = Mathf.Abs(disZ);

        if (disx > disz)
        {
            hor = true;
            float xBlocks = (disx - colSize.x/2) / colSize.x;
            int xBlock = Mathf.CeilToInt(xBlocks);
            amountBlocks = xBlock;
            float iets = line.GetPosition(1).x;
            float midpoint = (startPos.x + iets) / 2;
            Vector3 lenght = new Vector3(colSize.x * xBlock - 0.01f, colSize.y, colSize.z - 0.01f) / 2;
            Vector3 newPos = new Vector3(startPos.x - (colSize.x * xBlock) - colSize.x / 2, mouse3DPos.y, startPos.z);
            float hight = colSize.y / 2 + startPos.y;
            if (disX > 0)
            {
                horPos = true;
                line.SetPosition(0, new Vector3(startPos.x - colSize.x / 2, startPos.y + 0.01f, startPos.z));
                line.SetPosition(1, newPos);
                Collider[] notObscured = Physics.OverlapBox(new Vector3(midpoint, hight, startPos.z), lenght, Quaternion.identity, obstacleLayer);
                if (notObscured.Length > 0)
                {
                    lineMat.color = Color.red;
                    obstructed = true;
                }
                else
                {
                    lineMat.color = Color.green;
                    obstructed = false;
                }
            }
            else
            {
                newPos = new Vector3(startPos.x + (colSize.x * xBlock) + (colSize.x / 2), mouse3DPos.y, startPos.z);
                line.SetPosition(0, new Vector3(startPos.x + colSize.x / 2, startPos.y + 0.01f, startPos.z));
                line.SetPosition(1, newPos);
                Collider[] notObscured = Physics.OverlapBox(new Vector3(midpoint, hight, startPos.z), lenght, Quaternion.identity, obstacleLayer);
                if (notObscured.Length > 0)
                {
                    lineMat.color = Color.red;
                    obstructed = true;
                }
                else
                {
                    lineMat.color = Color.green;
                    obstructed = false;
                }
            }
        }
        else
        {

            float zBlocks = (disz - colSize.z / 2) / colSize.z;

            int zBlock = Mathf.CeilToInt(zBlocks);
            amountBlocks = zBlock;
            float iets = line.GetPosition(1).z;
            float midpoint = (startPos.z + iets) / 2;
            Vector3 lenght = new Vector3(colSize.x - 0.01f, colSize.y, colSize.z * zBlock - 0.01f) / 2;
            Vector3 newPos = new Vector3(startPos.x, mouse3DPos.y, startPos.z - (colSize.z * zBlock) - (colSize.z / 2));
            float hight = colSize.y / 2 + startPos.y;
            if (disZ > 0)
            {
                vertPos = true;
                line.SetPosition(0, new Vector3(startPos.x, startPos.y + 0.01f, startPos.z - colSize.z / 2));
                line.SetPosition(1, newPos);
                Collider[] notObscured = Physics.OverlapBox(new Vector3(startPos.x, hight, midpoint), lenght, Quaternion.identity, obstacleLayer);
                if (notObscured.Length > 0)
                {
                    lineMat.color = Color.red;
                    obstructed = true;
                }
                else
                {
                    lineMat.color = Color.green;
                    obstructed = false;
                }
            }
            else
            {
                newPos = new Vector3(startPos.x, mouse3DPos.y+0.01f, startPos.z + (colSize.z * zBlock) + (colSize.z / 2));
                line.SetPosition(0, new Vector3(startPos.x, startPos.y + 0.01f, startPos.z + colSize.z / 2));
                line.SetPosition(1, newPos);
                Collider[] notObscured = Physics.OverlapBox(new Vector3(startPos.x, hight, midpoint), lenght, Quaternion.identity, obstacleLayer);
                if (notObscured.Length > 0)
                {
                    lineMat.color = Color.red;
                    obstructed = true;
                }
                else
                {
                    lineMat.color = Color.green;
                    obstructed = false;
                }
            }
        }
        if (!obstructed && Input.GetButtonDown("Fire1") && notFirstFrame)
        {

            Vector3 startPoint = new Vector3(0, 0, 0);
            float hori = 0;
            if (hor)
            {
                
                if (horPos)
                {
                    hori = startPos.x;
                    for (int i = 0; i < amountBlocks; i++)
                    {
                        hori -= colSize.x;
                        allRoadsToPlace.Add(Instantiate(allBuildings[inndex], new Vector3(hori, startPos.y, startPos.z), Quaternion.identity));
                        startRoad = new Vector3(hori, startPos.y, startPos.z);
                    }

                }
                else
                {
                    hori = startPos.x;
                    for (int i = 0; i < amountBlocks; i++)
                    {
                        hori += colSize.x;
                        allRoadsToPlace.Add(Instantiate(allBuildings[inndex], new Vector3(hori, startPos.y, startPos.z), Quaternion.identity));
                        startRoad = new Vector3(hori, startPos.y, startPos.z);
                    }
                }
            }
            else
            {
                if (vertPos)
                {
                    hori = startPos.z;
                    for (int i = 0; i < amountBlocks; i++)
                    {
                        hori -= colSize.z;
                        allRoadsToPlace.Add(Instantiate(allBuildings[inndex], new Vector3(startPos.x, startPos.y, hori), Quaternion.identity));
                        startRoad = new Vector3(startPos.x, startPos.y, hori);
                    }

                }
                else
                {
                    hori = startPos.z;
                    for (int i = 0; i < amountBlocks; i++)
                    {
                        hori += colSize.z;
                        allRoadsToPlace.Add(Instantiate(allBuildings[inndex], new Vector3(startPos.x, startPos.y, hori), Quaternion.identity));
                        startRoad = new Vector3 (startPos.x,startPos.y,hori);
                    }
                }
            }
            for (int i = 0; i < allRoadsToPlace.Count; i++)
            {
                if(i != 0)
                {
                    allRoadsToPlace[i].GetComponentInChildren<Building>().MyStart();
                }
                if(i < allRoadsToPlace.Count - 1)
                {
                    allRoadsToPlace[i].GetComponentInChildren<Building>().Place();
                }
            }
            GameObject lastOne = allRoadsToPlace[allRoadsToPlace.Count - 1];
            allRoadsToPlace.Clear();
            allRoadsToPlace.Add(lastOne);
            continuePlacing = true;
        }
        notFirstFrame = true;
        if (Input.GetButtonDown("Fire2"))
        {
            placingRoad = false;
            road = false;

            for (int i = 0; i < allRoadsToPlace.Count; i++)
            {
                if(continuePlacing && i == 0)
                {
                    allRoadsToPlace[i].GetComponentInChildren<Building>().Place();
                }
                else if (allRoadsToPlace[i] != null)
                {
                    Destroy(allRoadsToPlace[i]);
                }
            }
            line.enabled = false;
            notFirstFrame = false;
            allRoadsToPlace.Clear();
            continuePlacing = false;
        }
    }
}
