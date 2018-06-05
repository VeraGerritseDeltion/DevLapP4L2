using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumber : MonoBehaviour {

	public int myRadius;
	public List<Collider> treeList;
	public LayerMask tree;

	void Start () {
		AddTrees();
	}
	void AddTrees(){
		Collider[] trees = Physics.OverlapSphere(transform.position, myRadius, tree);
		treeList = new List<Collider>(trees);


	}

	IEnumerator TreeTime(){
		yield return new WaitForSeconds(Random.Range(2,5));
		treeList.Remove(treeList[Random.Range(0, treeList.Count)]);
	}
}
