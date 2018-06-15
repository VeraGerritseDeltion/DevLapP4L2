using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static EventManager instance;

    public List<Events> allEvents = new List<Events>();

	void Awake ()
    {
		if(instance == null)
        {
            instance = this;
        }
	}

	void Update () {
		
	}

    public void StartEvent()
    {
        List<Events> posEvents = new List<Events>();
        if (allEvents.Count != 0)
        {
            for (int i = 0; i < allEvents.Count; i++)
            {
                if (allEvents[i].Posibility())
                {
                    posEvents.Add(allEvents[i]);
                }
            }
        }
        if(posEvents.Count != 0)
        {
            int rand = Random.Range(0, posEvents.Count);
            posEvents[rand].Occur();
        }
    }
}
