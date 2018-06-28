using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumber : Building {

	public int myRadius;
	public List<Collider> treeList;
	public List<Collider> usedList;
	public LayerMask tree;

	public bool cutting;

	public override void LumberAndCrops()
	{
        AddTrees();
    }
	void AddTrees()
	{
        Collider[] trees = Physics.OverlapSphere(transform.position,myRadius,tree);
		treeList = new List<Collider>(trees);
		
		StartCoroutine(CutTrees());
		StartCoroutine(PlantTrees());
	}

	IEnumerator CutTrees()
	{
		if(treeList.Count >= 1 && cutting)
		{
			int random = Random.Range(0, treeList.Count);
			int randomTransY = Random.Range(0,360);

			treeList[random].GetComponent<Animation>().Play("Cutting");
			yield return new WaitForSeconds(treeList[random].GetComponent<Animation>().clip.length);
			treeList[random].GetComponent<Animation>().Stop();

			treeList[random].transform.localScale = new Vector3 (0,0,0);
			treeList[random].transform.eulerAngles = new Vector3(0,randomTransY,0);

			usedList.Add(treeList[random]);
			treeList.Remove(treeList[random]);
		}
		yield return new WaitForSeconds(0.1f);
		StartCoroutine(CutTrees());
	}

	IEnumerator PlantTrees()
	{
		if(usedList.Count >= 1 && !cutting)
		{
			int random = Random.Range(0, usedList.Count);

			usedList[random].GetComponent<Animation>().Play("Growing");
			yield return new WaitForSeconds(usedList[random].GetComponent<Animation>().clip.length);

			treeList.Add(usedList[random]);
			usedList.Remove(usedList[random]);
		}
		yield return new WaitForSeconds(0.1f);
		StartCoroutine(PlantTrees());
	}
}
