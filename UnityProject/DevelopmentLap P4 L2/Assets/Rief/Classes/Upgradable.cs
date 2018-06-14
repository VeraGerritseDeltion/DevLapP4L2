using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour {


	public GameObject upgradeMesh;
	void Start () {
		Upgrade();
	}
	
	void Update () {
		
	}

	public void Upgrade()
	{
		Mesh myMesh = upgradeMesh.GetComponent<MeshFilter>().sharedMesh;
		GetComponent<MeshFilter>().mesh = myMesh;
	}
}
