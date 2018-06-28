using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodYear : Events {
    public float duration;
    public float decreaseFoodPro;

    public override void Occur()
    {
        base.Occur();
        StatisticManager.instance.foodEventBasedProductionLevel += decreaseFoodPro;
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        StatisticManager.instance.foodEventBasedProductionLevel -= decreaseFoodPro;
    }
}
