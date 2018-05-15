using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public bool isPlaced;
    public Material myMat;
    public bool inOtherBuilding;
    public LayerMask obstacles;
    Color normalColor;
	void Start () {
        myMat = GetComponent<Renderer>().material;
        normalColor = myMat.color;
	}
	
	void Update () {
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
        Collider[] buildings = Physics.OverlapSphere(transform.position,1,obstacles);
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
}
