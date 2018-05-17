using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlacement : MonoBehaviour {

    [Header("testVar")]
    public GameObject testBuilding;
    public LayerMask obstacleLayer;
    public LayerMask groundLayer;
    bool startedPlacing;
    GameObject isPlacing;
    Building placing;

	void Start () {
		
	}
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.P)|| startedPlacing)
        {
            PlaceBuilding();
        }
	}

    void PlaceBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 mouse3DPos = new Vector3(0,0,0);
        if (Physics.Raycast(ray, out hit,1000,groundLayer))
        {
            mouse3DPos = hit.point;
        }
        else
        {
            return;
        }

        if (!startedPlacing)
        {
            startedPlacing = true;
            isPlacing = Instantiate(testBuilding, mouse3DPos, Quaternion.identity);
            placing = isPlacing.GetComponent<Building>();
            if(placing == null)
            {
                placing = isPlacing.GetComponentInChildren<Building>();
            }
            placing.obstacles = obstacleLayer;
        }
        if(isPlacing != null && startedPlacing)
        {
            isPlacing.transform.position = mouse3DPos;
            if(Input.GetButtonDown("Fire1") && !placing.inOtherBuilding)
            {
                placing.Place();
                isPlacing = null;
                startedPlacing = false;
                placing = null;
            }
        }
    }
}
