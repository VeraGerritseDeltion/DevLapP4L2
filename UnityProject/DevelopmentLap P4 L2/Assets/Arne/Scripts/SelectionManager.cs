using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

	public Camera cam;
	public GameObject currentSelected;

	//the color/material it should change to
	public Color highlightColor;
	public Material highlightMaterial;

	//the material of the selected object and a save of it
	public Material selectedMaterial;
	public Material selectedSavedMaterial;

	// Use this for initialization
	void Start () 
	{
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	// Update is called once per frame
	void Update () 
	{
		CheckInput();
	}
	public void CheckInput () 
	{
		
		//make currentselected the object hit, show a highlight and open menu(tooltip)
		if(Input.GetButtonDown("Fire1"))
		{
			CheckHit();
		}
		if(Input.GetButtonDown("Fire2"))
		{
			if(currentSelected == null)
			{
				//extra options or something?
			}
			if(currentSelected != null)
			{
				currentSelected = null;
			}
		}
	}
	//shoots ray and checks hit
	public void CheckHit ()
	{
		RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit)) //maybe hit with layermask to avoid any unnecesary stuff
		{
            if(hit.collider.tag == "Building")
			{
				print("hit something");
				currentSelected = hit.collider.gameObject;


				//currentselected should be highlighted
				//highlight should go via object itself
				//scriptable object.sound should be played

				//show tooltip

			}
			else
			{
				
				//nothing
			}
		}

	}
}
