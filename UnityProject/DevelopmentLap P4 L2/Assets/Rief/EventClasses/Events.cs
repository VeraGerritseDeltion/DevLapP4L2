using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {
    public string eventWord;
    public int exhaustNeeded;
    public int happynessNeeded;
    public bool CanAlwaysOccur;
    public bool goodOrBad;

    public virtual void Occur()
    {
        UIManager.instance.EventLog("Year " + StatisticManager.instance.age.ToString() + ": " + eventWord);
    }
        
    public virtual bool Posibility()
    {
        bool canOccur = false;
        if (CanAlwaysOccur)
        {
            canOccur = true;
        }
        if (goodOrBad)
        {
            //good
            if (exhaustNeeded >= NatureManager.instance.SendExhaust())
            {
                canOccur = true;
            }
            if (happynessNeeded <= StatisticManager.instance.avrHappiness)
            {
                canOccur = true;
            }
        }
        else
        {
            //bad
            if (exhaustNeeded <= NatureManager.instance.SendExhaust())
            {
                canOccur = true;
            }
            if (happynessNeeded >= StatisticManager.instance.avrHappiness && StatisticManager.instance.avrHappiness != 0)
            {
                canOccur = true;
            }
        }

        return canOccur;
    }
}
