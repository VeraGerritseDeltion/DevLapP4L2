using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : MonoBehaviour {



//moet nog UI links hebben.


	public bool wheatBool;
	public GameObject wheat;
	public GameObject carrot;

	GameObject spawned;
	bool isGrowing = false;
	public float growSpeed;
	public int harvestAmount;
	public int moneyCost;


	void Update()
	{
		Growing();
	}

	public void Spawning()
	{
		isGrowing = false;
		StatisticManager.instance.money -= moneyCost * harvestAmount;

		if(harvestAmount > 0)
		{	harvestAmount --;
			if(isGrowing == false)
			{
				isGrowing = true;
				if(wheatBool)
				{
					spawned = Instantiate(wheat, new Vector3(transform.position.x, transform.position.y-0.6f, transform.position.z), Quaternion.Euler(-90, transform.rotation.y, transform.rotation.z));
				} 
				else
				{
					spawned = Instantiate(carrot, new Vector3(transform.position.x, transform.position.y-0.2f, transform.position.z), Quaternion.Euler(-90, transform.rotation.y, transform.rotation.z));
				}
			}
		}
	}

	void Growing()
	{		
		if(spawned != null){
			Vector3 targetLoc = new Vector3(spawned.transform.position.x, 1, spawned.transform.position.z);
			spawned.transform.position = Vector3.MoveTowards(spawned.transform.position, targetLoc, growSpeed * Time.deltaTime);

			if(spawned.transform.position == targetLoc)
			{
				Harvest();
			}
		}
	}

	public void Harvest()
	{
		Destroy(spawned.gameObject);
		Spawning();
	}

	public void HarvestUp()
	{
		harvestAmount++;
	}
	
	public void HarvestDown()
	{
		harvestAmount--;
	}
}
