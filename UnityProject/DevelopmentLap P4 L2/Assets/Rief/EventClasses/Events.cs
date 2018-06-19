using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {
    public string eventWord;

    public virtual void Occur()
    {
        UIManager.instance.EventLog("Year " + StatisticManager.instance.age.ToString() + " : " + eventWord);
    }

    public bool Posibility()
    {
        return true;
    }
}
