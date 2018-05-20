using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Building", menuName = "New Building/Building")]
public class BuildingTemplate : ScriptableObject{

    public string name;
    public string description;

    public Sprite buildingSprite;

    [Header("Plus")]
    public int plusWood;
    public int plusStone;
    public int plusMoney;
    public int plusHappiness;
    public int plusCo2;
    public int plusWater;
    public int plusEnergy;

    [Header("Minus")]
    public int minWood;
    public int minStone;
    public int minMoney;
    public int minHappiness;
    public int minCo2;
    public int minWater;
    public int minEnergy;

}
