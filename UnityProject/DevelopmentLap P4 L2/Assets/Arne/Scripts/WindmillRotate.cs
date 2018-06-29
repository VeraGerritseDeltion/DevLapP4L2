using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillRotate : MonoBehaviour {

	float rot;
	public float rotSpeed;
    public bool power;


    void Update () 
	{
		if(Time.deltaTime != 0)
		{
			
			if(!power)
			{
				rot -= (1 * Time.deltaTime * rotSpeed);
				gameObject.transform.localRotation = Quaternion.Euler(new Vector3(rot, 90, 90));
			}
			else
			{
				rot += (1 * Time.deltaTime * rotSpeed);
                gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rot));
            }
			
			
		}
	}
}
