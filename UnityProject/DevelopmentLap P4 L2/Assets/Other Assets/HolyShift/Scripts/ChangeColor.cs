using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    public Color startColor;
    public Color endColor;
    public Color currColor;

    void Start () {
        ChangingColor();
        
    }
	
	void Update () {
		
	}
    void ChangingColor(){
        currColor = Color.Lerp(startColor, endColor, NatureManager.instance.uitstoot / 100);
		GetComponent<Renderer>().material.SetColor("_BaseColor", currColor);
    }
}
