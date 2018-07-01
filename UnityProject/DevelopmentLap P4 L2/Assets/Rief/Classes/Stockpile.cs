using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : MonoBehaviour {


    public GameObject woodPileOne;
	public GameObject woodPileTwo;
    public GameObject stonePileOne;
	public GameObject stonePileTwo;
	int woodPerc;
	int stonePerc;
	
	void Update () 
	{
        SetActive();
    }
	void SetActive()
	{
		
		if(StatisticManager.instance.wood > 100)
		{
            woodPileOne.SetActive(true);
        }
		else
		{
            woodPileOne.SetActive(false);
        }

		if(StatisticManager.instance.stone > 100)
		{
            stonePileOne.SetActive(true);
        }
		else
		{
            stonePileOne.SetActive(false);
        }
		if(StatisticManager.instance.wood > 200)
		{
			woodPileTwo.SetActive(true);
		}
		else
		{
			woodPileTwo.SetActive(false);
		}
		if(StatisticManager.instance.stone > 200)
		{
			stonePileTwo.SetActive(true);
		}
		else
		{
			stonePileTwo.SetActive(false);
		}
	}
}
