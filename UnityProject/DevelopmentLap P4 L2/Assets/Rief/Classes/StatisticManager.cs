﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour {
    public static StatisticManager instance;

    private float statTimer = 1f;
    public int age;
    public float timeForAge;

    public List<GameObject> workBuildings = new List<GameObject>();
    public int usedCitizens;
    public List<int> homeless = new List<int>();

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
    public float wood;
    public float stone;
    public float money;
    public float food;
    public float minerals;
    public int startHappiness;
    public int homelessDecay;
    public float happiness;
    public float avrHappiness;
    public float water;
    public float energy;
    public float co2;
    public int citizens;
    public int allCitizens;

    [Header ("Storage")]
    public int woodStorage;
    public int stoneStorage;
    public int moneyStorage;
    public int foodStorage;



    [Header("Event chance")]
    public float startChanceEvent;
    float chanceEvent;

    [Header("Production")]
    public float productionLevel;
    public float foodEventBasedProductionLevel;

    
    void Awake() 
    {
		if(instance == null) {
            instance = this;
        }
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            money += 500;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            food += 500;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            wood += 500;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            stone += 500;
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            EventManager.instance.StartEvent();
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            co2 += 100;
            StatsChanged();
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            co2 -= 100;
            StatsChanged();
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            happiness += 50;
            AverageHappiness();
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            foodEventBasedProductionLevel += 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            UIManager.instance.EventLog("Fire Started");
            for (int i = 0; i < 5; i++)
            {
                NatureManager.instance.StartFire();
            }
        }
    }

    public void MyStart()
    {
        foodEventBasedProductionLevel = 1;
        chanceEvent = startChanceEvent;
        StartCoroutine(AddStats());
        StartCoroutine(AgeIncrease());
    }

    public float AverageHappiness()
    {
        if(allCitizens >0)
        {
            avrHappiness = happiness / allCitizens;
        }
        return (Mathf.CeilToInt(avrHappiness + 50) / 100);
    }

    IEnumerator AddStats ()
    {
        yield return new WaitForSeconds (statTimer);
        productionLevel = AverageHappiness();
        wood += Mathf.RoundToInt(addWood * productionLevel);
        stone += Mathf.RoundToInt(addStone * productionLevel);
        money += Mathf.RoundToInt(addMoney * productionLevel);
        minerals += Mathf.RoundToInt(addMinerals * productionLevel);
        food += Mathf.RoundToInt(addFood * productionLevel * foodEventBasedProductionLevel);

        happiness += addHappiness;
        water += Mathf.RoundToInt(addWater * productionLevel);
        energy += Mathf.RoundToInt(addEnergy * productionLevel);
        co2 = addCo2;
        StatsChanged();
        UIManager.instance.TextUpdate();
        StartCoroutine(AddStats());
    }

    IEnumerator AgeIncrease()
    {
        yield return new WaitForSeconds(timeForAge);
        age++;
        for (int i = 0; i < homeless.Count; i++)
        {
            if (homeless[i] > 0)
            {
                homeless[i] -= homelessDecay;
            }
        }
        happiness -= (homeless.Count * homelessDecay);
            if (age > 3)
            {
                Event();
            }
        StartCoroutine(AgeIncrease());
    }

    public void StatsChanged()
    {
        NatureManager.instance.CalculateProcent(co2);
    }

    void Event()
    {
        int rand = Random.Range(0, 100);
        if ( rand < chanceEvent)
        {
            chanceEvent = startChanceEvent;
            EventManager.instance.StartEvent();
        }
        else
        {
            chanceEvent += 10;
        }
    }
}
