using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : MonoBehaviour {



//moet nog UI references hebben


	public bool wheatBool;
	public GameObject wheat;
	public GameObject carrot;

	GameObject spawned;

	void Start()
	{
		Spawning();
	}
	public void Spawning()
	{
		if(wheatBool)
		{
			spawned = Instantiate(wheat, new Vector3(transform.position.x, transform.position.y-0.6f, transform.position.z), Quaternion.Euler(-90, transform.rotation.y, transform.rotation.z));
			
		} 
		else
		{
			spawned = Instantiate(carrot, new Vector3(transform.position.x, transform.position.y-0.2f, transform.position.z), Quaternion.Euler(-90, transform.rotation.y, transform.rotation.z));
			
		}
	}
	public void Harvest(){

		Destroy(spawned.gameObject);
	}
}
