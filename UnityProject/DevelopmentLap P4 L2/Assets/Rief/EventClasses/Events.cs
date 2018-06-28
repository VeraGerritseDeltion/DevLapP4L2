using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {
    public string eventWord;
    public int exhaustNeeded;
    //public int 

    public virtual void Occur()
    {
        UIManager.instance.EventLog("Year " + StatisticManager.instance.age.ToString() + " : " + eventWord);
    }

    public bool Posibility()
    {
        bool canOccur = false;
        if(exhaustNeeded< NatureManager.instance.SendExhaust())
        {
            canOccur = true;
        }
        return true;
    }
}
