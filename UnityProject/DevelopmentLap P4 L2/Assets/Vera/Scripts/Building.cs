using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public bool isPlaced;
    public Material myMat;
    public bool inOtherBuilding;
    public LayerMask obstacles;
    Color normalColor;

    BoxCollider myCol;
    Vector3 sizeCol;

	void Start ()
    {
        myMat = GetComponent<Renderer>().material;
        normalColor = myMat.color;
        myCol = GetComponentInChildren<BoxCollider>();
        sizeCol = new Vector3(myCol.size.x, myCol.size.z, myCol.size.y)/2;
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
        isPlaced = true;
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
