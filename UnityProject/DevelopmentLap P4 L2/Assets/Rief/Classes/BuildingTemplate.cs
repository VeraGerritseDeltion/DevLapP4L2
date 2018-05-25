using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Building", menuName = "New Building/Building")]
public class BuildingTemplate : ScriptableObject {

    public new string name;
    public string description;

    public Sprite buildingSprite;
    
    public int wood;
    public int stone;
    public int money;
    public int minerals;
    public int food;
    public int happiness;
    public int co2;
    public int water;
    public int energy;

}
