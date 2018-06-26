using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Happiness : MonoBehaviour {

	public Color red, yellow, green;
	public Image icon;
	public float number;

	void Update ()
	{
		HappinessColor();
	}
	public void HappinessColor ()
	{
		if(number < 33f)
		{
			icon.color = red;
		}
		if(number > 33f && number < 66f)
		{
			icon.color = yellow;
		}
		if(number > 66f)
		{
			icon.color = green;
		}
	}
}
