using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropScript : MonoBehaviour {


	public float growSpeed;

	public bool harvest;
	void Start () {
	}
	
	void Update () {
		MoveUp();
	}

	public void MoveUp()
	{
		Vector3 targetLoc = new Vector3(transform.position.x, 1, transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, targetLoc, growSpeed * Time.deltaTime);

		if(transform.position == targetLoc)
		{
			harvest = true;
		}
	}
}
