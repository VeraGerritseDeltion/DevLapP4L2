using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLoc : MonoBehaviour {
    
	void Start () {
        TreeInstantiationManager.instance.treeLoc.Add(transform);
	}
	
	void Update () {
		
	}
}
