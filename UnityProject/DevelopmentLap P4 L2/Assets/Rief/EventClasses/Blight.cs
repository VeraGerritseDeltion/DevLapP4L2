using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blight : Events {
    public float duration;
    public float decreaseFoodPro;

    public override void Occur()
    {
        base.Occur();
        StatisticManager.instance.foodEventBasedProductionLevel -= decreaseFoodPro;
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        UIManager.instance.EventLog("The blight has gone away");
        StatisticManager.instance.foodEventBasedProductionLevel += decreaseFoodPro;
    }
}
