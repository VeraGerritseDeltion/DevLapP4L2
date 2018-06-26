using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Building building;
	public TooltipView viewTooltip;
	public GameObject thisGO;
	public bool stats;
	public string desc;

	void Start () 
	{
		thisGO = this.gameObject;
	}
	public void OnPointerEnter(PointerEventData eventData)
    {
		if(stats)
		{
			viewTooltip.SetPosition(new Vector3(thisGO.transform.position.x, thisGO.transform.position.y -60f), building, desc);
		}
		if(!stats)
		{
			viewTooltip.SetPosition(new Vector3(thisGO.transform.position.x, thisGO.transform.position.y +105f), building, desc);
		}
		//viewTooltip.SetPosition(new Vector3(thisGO.transform.position.x, thisGO.transform.position.y +105f), building, desc); //eventData.position.x, eventData.position.y - -100f, 0f
		viewTooltip.Show(true);
    }
	public void OnPointerExit(PointerEventData eventData)
    {
		viewTooltip.Show(false);
    }
}
