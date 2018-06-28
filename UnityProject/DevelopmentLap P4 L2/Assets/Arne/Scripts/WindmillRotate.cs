using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillRotate : MonoBehaviour {

	float rot;
	public float rotSpeed;

	
	void Update () 
	{
		if(Time.deltaTime != 0)
		{
			rot -= (1 * Time.deltaTime * rotSpeed);
			gameObject.transform.localRotation = Quaternion.Euler(new Vector3(rot, 90 ,90));
		}
	}
}
