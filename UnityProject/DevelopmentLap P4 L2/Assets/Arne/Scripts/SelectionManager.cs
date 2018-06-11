using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectionManager : MonoBehaviour {

	public Camera cam;
	public GameObject currentSelected;

	//the color/material it should change to
	public Color highlightColor;
	public Material highlightMaterial;

	//the material of the selected object and a save of it
	public Material selectedMaterial;
	public Material selectedSavedMaterial;

	public List<Building> allBuildings = new List<Building>();
	public List<Trees> allTree = new List<Trees>();


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
				BuildingStuff();
				TreeStuff();
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
            if(hit.collider.tag == "Building" || hit.collider.tag == "Obstacle")
			{
				print("hit something");
				currentSelected = hit.collider.gameObject;
				
				Trees tree = currentSelected.GetComponent<Trees>();
				Building build = currentSelected.GetComponent<Building>();
				CheckBuildingList(build);
				CheckTreeList(tree);


				BuildingStuff();
				TreeStuff();
				if(build != null)
				{
					build.Tooltip(true);
					build.HighlightBuilding(true);
				}
				if(tree != null)
				{
					tree.Tooltip(true);
					Debug.Log("turn on tree tt");
					//tree.Highlight(true);
				}
				
					
				

				//currentselected should be highlighted
				//highlight should go via object itself
				//scriptable object.sound should be played

				//show tooltip
				
			}
			else
			{
				//print("else");
				//nothing
			}
		}
		//print("run");
	}
	public void TooltipStuff () 
	{

	}
	public void BuildingStuff () 
	{
		foreach (var item in allBuildings)
		{
			if(item == null)
			{
				//return;
			}
			if(item != null)
			{
				item.Tooltip(false);
			}
		}
	}
	public void TreeStuff () 
	{
		foreach (var item in allTree)
		{
			if(item == null)
			{
				//return;
			}
			if(item != null)
			{
				item.Tooltip(false);
			}
		}
	}
	public void CheckBuildingList (Building building) 
	{
		allBuildings.Add(building);
	 	allBuildings = allBuildings.Distinct().ToList();
	}
	public void CheckTreeList (Trees tree)
	{
		allTree.Add(tree);
	 	allTree = allTree.Distinct().ToList();
	}
}
