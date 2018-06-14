﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour {
    public static StatisticManager instance;

    private float statTimer = 1f;
    public int age;
    public float timeForAge;

    [Header ("Adding")]
    public int addWood;
    public int addStone;
    public int addMoney;
    public int addFood;
    public int addMinerals;
    public int addHappiness;
    public int addWater;
    public int addEnergy;
    public int addCo2;


    [Header ("Stats")]
    public int wood;
    public int stone;
    public int money;
    public int food;
    public int minerals;
    public int happiness;
    public int water;
    public int energy;
    public int co2;

    [Header ("Storage")]
    public int woodStorage;
    public int stoneStorage;
    public int moneyStorage;
    public int foodStorage;

    void Awake() 
    {
		if(instance == null) {
            instance = this;
        }
	}

    public void MyStart()
    {
        StartCo();
    }

    void Update()
    {
        AgeIncrease();
    }

    void StartCo ()
    {
        StartCoroutine (AddStats ());
    }

    IEnumerator AddStats ()
    {

        yield return new WaitForSeconds (statTimer);
        wood += addWood;
        stone += addStone;
        money += addMoney;
        minerals += addMinerals;
        food += addFood;

        happiness += addHappiness;
        water += addWater;
        energy += addEnergy;
        co2 += addCo2;
        StartCo ();
    }

    void AgeIncrease()
    {
        timeForAge -= 1 * Time.deltaTime;

        if(timeForAge <= 0)
        {
            age++;
        }
    }
}
