﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager instance;

    public List<GameObject> allBuildings;

    void Awake()
	{
		if(instance == null){
            instance = this;
        }
	}
}