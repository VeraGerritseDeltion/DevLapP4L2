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



    void Start () {
		
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
            if(placing == null)
            {
                
                placing = isPlacing.GetComponentInChildren<Building>();
            }
            //placing.MyStart();
            placing.obstacles = obstacleLayer;
        }
        
        if(isPlacing != null && startedPlacing)
        {
            if (Input.GetButton("LeftShift"))
            {
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
                    print("Ugh");
                    startRoad = mouse3DPos;
                    roadToBePlaced = toBePlaced;
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
            //closeBuild = GetComponent<Building>();
            Vector3 closeColSize = closeBuild.GetColliderSize();
            Vector3 posClosest = closest.transform.position;
            float disX = posClosest.x - mousePos.x;
            float disZ = posClosest.z - mousePos.z;
            Vector3 snappedPos = new Vector3 (0,0,0);
            float disx = Mathf.Abs (disX);
            float disz = Mathf.Abs (disZ);
            bool trueSnap = false;
            if (Input.GetButton("LeftControl"))
            {
                trueSnap = true;
            }
            if (disx > disz)
            {
                if(disX > 0)
                {
                    snappedPos = new Vector3(posClosest.x - sizeCol.x - closeColSize.x - 0.01f, mousePos.y, mousePos.z);
                    if (trueSnap)
                    {
                        snappedPos.z = posClosest.z;
                    }
                }
                else
                {
                    snappedPos = new Vector3(posClosest.x + sizeCol.x + closeColSize.x + 0.01f, mousePos.y, mousePos.z);
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
                    snappedPos = new Vector3(mousePos.x, mousePos.y, posClosest.z - sizeCol.z - closeColSize.z - 0.01f);
                    if (trueSnap)
                    {
                        snappedPos.x = posClosest.x;
                    }
                }
                else
                {
                    snappedPos = new Vector3(mousePos.x, mousePos.y, posClosest.z + sizeCol.z + closeColSize.z + 0.01f);
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
        bool newRoad = false;
        Building newBuilding = allBuildings[whichBuilding].GetComponent<Building>();
        if(newBuilding.GetType() == typeof(Road))
        {
            print("Road");
            newRoad = true;
        }
        placingRoad = newRoad;
        if(whichBuilding < allBuildings.Count)
        {
            PlaceBuilding(allBuildings[whichBuilding],whichBuilding,newRoad);
        }
    }

    void PlaceRoad(GameObject toBePlaced, int inndex, Vector3 startPos)
    {

    }
}
