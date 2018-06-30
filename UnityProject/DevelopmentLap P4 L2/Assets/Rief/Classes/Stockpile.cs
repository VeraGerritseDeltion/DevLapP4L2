using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : MonoBehaviour {


    public GameObject woodPile;
    public GameObject stonePile;
	
	void Update () 
	{
        SetActive();
    }
	void SetActive()
	{
		if(StatisticManager.instance.wood > 0)
		{
            woodPile.SetActive(true);
        }
		else
		{
            woodPile.SetActive(false);
        }

		if(StatisticManager.instance.stone > 0)
		{
            stonePile.SetActive(true);
        }
		else
		{
            stonePile.SetActive(false);
        }
	}
}
