using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeUI : MonoBehaviour {

    Vector3 scale;
	void Awake () {
        scale = transform.localScale;
	}

    private void Start()
    {
        UIManager.instance.allUIFromBuildings.Add(this);
        ResizeShiz(GameManager.instance.zoom);
    }

    public void ResizeShiz(float procent)
    {
        transform.localScale = scale * procent;
    }
	void Update () {
		
	}
}
