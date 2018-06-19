using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingCost : MonoBehaviour {

	public GameObject thisbuilding;
	public TextMeshProUGUI costText;
	public int woodCost, stoneCost, moneyCost;
	public RectTransform tooltip;
	
	void OnMouseOver () 
	{
		//set the position
		
		//sets the cost
		woodCost = thisbuilding.GetComponent<Building>().woodCost;
		stoneCost = thisbuilding.GetComponent<Building>().stoneCost;
		moneyCost = thisbuilding.GetComponent<Building>().moneyCost;

		costText.text = "Cost: " + "\n"  + "Wood: " + woodCost.ToString() + "\n" + "Stone: " + stoneCost.ToString() + "\n" + "Money: " + moneyCost.ToString();
	}
}
