﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosBrand : Events {
    public int amountTrees;
    public override void Occur()
    {
        base.Occur();
        for (int i = 0; i < amountTrees; i++)
        {
            NatureManager.instance.StartFire();
        }
    }
}
