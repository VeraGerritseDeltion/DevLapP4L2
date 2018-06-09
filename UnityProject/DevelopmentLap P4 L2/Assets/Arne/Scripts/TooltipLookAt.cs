using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipLookAt : MonoBehaviour {

    private Transform camTransform;
    	
	// Use this for initialization
	void Start () 
	{	
		camTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.LookAt(2 * transform.position - camTransform.transform.position);
	}
}
