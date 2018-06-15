using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    public Color startColor;
    public Color endColor;
    public Color currColor;

    void Start () {
        NatureManager.instance.waterColors.Add(this);
        ChangingColor(0);
        
    }
	
	void Update () {
		
	}
    public void ChangingColor(float percentage){
        currColor = Color.Lerp(startColor, endColor, percentage);
		GetComponent<Renderer>().material.SetColor("_BaseColor", currColor);
    }
}
