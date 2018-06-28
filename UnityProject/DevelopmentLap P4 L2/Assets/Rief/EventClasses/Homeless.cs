using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homeless : Events {

    public override bool Posibility()
    {
        return StatisticManager.instance.homeless.Count > 0;
    }
    public override void Occur()
    {
        UIManager.instance.EventLog("Year " + StatisticManager.instance.age.ToString() + " : " + StatisticManager.instance.homeless.Count.ToString() + eventWord);
    }
}
