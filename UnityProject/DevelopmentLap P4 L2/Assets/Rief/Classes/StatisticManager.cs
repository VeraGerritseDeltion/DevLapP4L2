using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour {
    public static StatisticManager instance;

    public int addWood;
    public int wood;
    public int addStone;
    public int stone;
    public int addMoney;
    public int Money;
    public int addMinerals;
    public int minerals;

	void Awake() 
    {
		if(instance == null) {
            instance = this;
        }
	}
	
	void Update () {
		
	}
}
