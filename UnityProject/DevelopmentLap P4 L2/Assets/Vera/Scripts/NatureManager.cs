using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureManager : MonoBehaviour
{
    public static NatureManager instance;
    public LayerMask ground;
    public float uitstoot;
    public float maxExhaust;
    public float currentExhaust;
    public int decOrInc;
    bool onOrOff;

    public List<ChangeColor> waterColors = new List<ChangeColor>();
    [Header("Start Colors nature")]
    public Color currentHigh;
    public Color currentLow;

    public Color startColorHigh;
    public Color startColorLow;
    [Header("Mid Colors nature")]
    public Color midColorHigh;
    public Color midColorLow;
    [Header("End Colors nature")]
    public Color endColorHigh;
    public Color endColorLow;

    [Header("forest fire")]
    public Color burnColorLow;
    public Color burnColorHigh;

    [Header("GrassColor")]
    public Color startGrass;
    public Color endGrass;
    Color currentGrass;
    public GameObject grass;

    Coroutine currentCo;
    float timer;

    [Header("Vegetation")]
    public List<GameObject> allTrees = new List<GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void MyStart () {
        CalculateProcent(0);
        StartCoroutine(UpdateTreesFast());
    }

    public void CalculateProcent(float exhaust)
    {
        bool LowerOrHigher = false;
        float procent = 0;
        if(exhaust != currentExhaust && currentCo == null)
        {
            currentCo =  StartCoroutine(SlowTimer(exhaust));
        }
        uitstoot = (currentExhaust / maxExhaust) * 100;
        ChangeGround(uitstoot / 100);
        if (uitstoot > 50)
        {
            float mid = uitstoot - 50;
            procent = mid / 50;
            LowerOrHigher = true;
        }
        else
        {
            procent = uitstoot/ 50;
        }
        for (int i = 0; i < waterColors.Count; i++)
        {
            waterColors[i].ChangingColor(uitstoot / 100);
        }
        ChangeNature(LowerOrHigher, procent);
    }
    public int SendExhaust()
    {
        return Mathf.RoundToInt (uitstoot);
    }

    IEnumerator SlowTimer(float exhaust)
    {
        yield return new WaitForSeconds(0.01f);
        if(exhaust < currentExhaust)
        {
            currentExhaust--;
        }
        else
        {
            currentExhaust++;
        }
        //currentExhaust = Mathf.Lerp(currentExhaust, exhaust, timer);
        currentCo = null;
        CalculateProcent(exhaust);
    }

    void ChangeGround(float procent)
    {
        currentGrass = Color.Lerp(startGrass, endGrass, procent);
        grass.GetComponent<Renderer>().material.color = currentGrass;
    }
    void ChangeNature(bool lower, float procent)
    {
        if (!lower)
        {
            currentHigh = Color.Lerp(startColorHigh, midColorHigh, procent);
            currentLow = Color.Lerp(startColorLow, midColorLow, procent);
        }
        else
        {
            currentHigh = Color.Lerp(midColorHigh, endColorHigh, procent);
            currentLow = Color.Lerp(midColorLow, endColorLow, procent);
        }
        for (int i = 0; i < allTrees.Count; i++)
        {
            float rand = allTrees[i].GetComponent<Trees>().myRand;
            Color newColor = Color.Lerp(currentLow, currentHigh, rand);
            allTrees[i].GetComponent<Trees>().ChangeColor(newColor);
        }
    }

    void UpdatesTrees()
    {

    }

    public void StartFire()
    {
        int rand = Random.Range(0, allTrees.Count);
        allTrees[rand].GetComponentInChildren<Trees>().StartFire();
    }

    IEnumerator UpdateTreesFast()
    {
        yield return new WaitForSeconds(0.1f);
        uitstoot += decOrInc;
        if(uitstoot < 0)
        {
            uitstoot = 0;
        }
        if(uitstoot > 100)
        {
            uitstoot = 100;
        }
        CalculateProcent(uitstoot);
        if (onOrOff)
        {
            StartCoroutine(UpdateTreesFast());
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(decOrInc == 0)
            {
                decOrInc = 1;
            }
            else if(decOrInc == 1)
            {
                decOrInc = -1;
            }
            else if(decOrInc == -1)
            {
                decOrInc = 1;
            }
            print(onOrOff);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            print(onOrOff);
            onOrOff = !onOrOff;
            if (onOrOff)
            {
                StartCoroutine(UpdateTreesFast());
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartFire();
        }
	}
}
