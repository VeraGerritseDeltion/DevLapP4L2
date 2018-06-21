using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipView : MonoBehaviour {

	//public GameObject thisbuilding;
	public TextMeshProUGUI costText;
	public int woodCost, stoneCost, moneyCost;
	public RectTransform tooltip;
	
	//sets position to value it receives and will show correct cost
	public void SetPosition (Vector3 position, Building building) 
	{
		tooltip.transform.position = position;
		ShowCost(building);
	}
	//sets the cost
	public void ShowCost (Building building) 
	{			
		woodCost = building.GetComponent<Building>().woodCost;
		stoneCost = building.GetComponent<Building>().stoneCost;
		moneyCost = building.GetComponent<Building>().moneyCost;

		costText.text = "Cost: " + "\n"  + "Money: " + moneyCost.ToString() + "\n" + "Stone: " + stoneCost.ToString() + "\n" + "Wood: " + woodCost.ToString();
	}
	//will enable/disable the tooltip
	public void Show(bool active)
	{
		this.gameObject.SetActive(active);
	}
}
