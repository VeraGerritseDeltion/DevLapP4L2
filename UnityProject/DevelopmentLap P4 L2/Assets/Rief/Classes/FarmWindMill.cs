using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmWindMill : MonoBehaviour {

	int myRadius = 5;
	public LayerMask farms;
	public List<Collider> myCropFields;

	void Placed()
	{
		Collider[] cropFields = Physics.OverlapSphere(transform.position, myRadius, farms);
		myCropFields = new List<Collider>(cropFields);

		for(int i = 0; i < myCropFields.Count; i++)
		{
			myCropFields[i].GetComponent<CropField>().fastSpeed *= 2;
		}
	}

	void RemoveMill()
	{
		for(int i = 0; i < myCropFields.Count; i++)
		{
			myCropFields[i].GetComponent<CropField>().fastSpeed /= 2;
			myCropFields.Remove(myCropFields[i]);
		}
	}
}
