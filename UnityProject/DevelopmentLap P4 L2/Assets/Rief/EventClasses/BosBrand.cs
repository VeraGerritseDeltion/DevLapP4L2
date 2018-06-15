using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosBrand : Events {
    public override void Occur()
    {
        base.Occur();
        NatureManager.instance.StartFire();
    }
}
