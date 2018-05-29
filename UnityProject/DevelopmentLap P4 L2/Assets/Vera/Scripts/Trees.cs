using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour {

    public Renderer myRend;
    public Material myMat;
    public float myRand;

	void Start () {
        NatureManager.instance.allTrees.Add(gameObject);
        myRend = GetComponentInChildren<Renderer>();
        myMat = myRend.material;

        float rand = Random.Range(0, 100);
        myRand = rand / 100;

    }

    public void ChangeColor(Color myNewColor)
    {
        myMat.color = myNewColor;
    }

	void Update () {
		
	}
}
