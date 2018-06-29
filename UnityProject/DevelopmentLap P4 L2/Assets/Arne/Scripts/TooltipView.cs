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
	public bool stats;
    public string buildingName;

    //sets position to value it receives and will show correct cost
    public void SetPosition (Vector3 position, Building building, string desc) 
	{
		tooltip.transform.position = position;
        if(building != null)
        {
			if(!stats)
			{
				ShowCost(building);
			}
        }
		if(stats)
		{
			ShowStats(desc);
		}
	}
	//sets the stats
	public void ShowStats (string desc) 
	{			

		costText.text = "Stats: " + "\n" + desc;
	}
	//sets the cost
	public void ShowCost (Building building) 
	{			
		woodCost = building.GetComponent<Building>().woodCost;
		stoneCost = building.GetComponent<Building>().stoneCost;
		moneyCost = building.GetComponent<Building>().moneyCost;
        buildingName = building.GetComponent<Building>().myBuilding.name;

        costText.text = "<b>" + buildingName + "</b>" + "\n" + "Money: " + moneyCost.ToString() + "\n" + "Stone: " + stoneCost.ToString() + "\n" + "Wood: " + woodCost.ToString();
	}
	//will enable/disable the tooltip
	public void Show(bool active)
	{
		this.gameObject.SetActive(active);
	}
}
