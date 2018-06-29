using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Happyness : Events {
    public List<string> text = new List<string>();

    public override void Occur()
    {
        UIManager.instance.EventLog(HappynessText());
    }

    public string HappynessText()
    {
        int avrHappyness = Mathf.RoundToInt(StatisticManager.instance.AverageHappiness() * 100 - 50);
        if (avrHappyness <= 25)
        {
            return text[0];
        }
        else if (avrHappyness <= 50)
        {
            return text[1];
        }
        else if (avrHappyness <= 75)
        {
            return text[2];
        }
        return text[3];
    }
}
