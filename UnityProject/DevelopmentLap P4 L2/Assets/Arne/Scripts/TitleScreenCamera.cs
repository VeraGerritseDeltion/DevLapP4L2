using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenCamera : MonoBehaviour {

	public float speed;
	// Update is called once per frame
	void FixedUpdate () {
		
		transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);
	}
}
