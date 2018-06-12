using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInstantiationManager : MonoBehaviour {
    public static TreeInstantiationManager instance;
    public List<Transform> treeLoc = new List<Transform>();
    public List<GameObject> treeKinds = new List<GameObject>();
	void Awake () {
		if(instance == null)
        {
            instance = this;
        }
	}

    public void MyStart()
    {
        StartCoroutine(PlaceTrees());
    }

    IEnumerator PlaceTrees()
    {
        yield return new WaitForSeconds(0.5f);
        if(treeLoc.Count != 0)
        {
            for (int i = 0; i < treeLoc.Count; i++)
            {
                yield return new WaitForSeconds(0.1f);
                int rand = Random.Range(0, treeKinds.Count);
                print(treeLoc[i].position);
                Instantiate(treeKinds[rand], treeLoc[i].position, Quaternion.identity);
            }
        }
    }
	
	void Update () {
		
	}
}
