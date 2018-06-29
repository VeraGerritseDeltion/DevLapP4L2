using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : Building
{



    //moet nog UI links hebben.


    public LayerMask windmill;

    public bool wheatBool;
    public GameObject wheat;
    public GameObject carrot;

    GameObject spawned;
    bool isGrowing = false;
    float growSpeed;
    public float fastSpeed;
    Vector3 targetLoc;

    int myRadius = 5;

    public override void LumberAndCrops()
    {
        FindWindmill();
    }

    void FindWindmill()
    {
        targetLoc = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        Collider[] myWindMills = Physics.OverlapSphere(transform.position, myRadius, windmill);
        for (int i = 0; i < myWindMills.Length - 1; i++)
        {
            myWindMills[i].GetComponent<FarmWindMill>().myCropFields.Add(GetComponent<Collider>());
        }
        //Spawning();
    }
    void Update()
    {
        Growing();
    }

    public void Spawning(bool growBool)
    {
        wheatBool = growBool;
        if (!wheatBool)
        {
			StatisticManager.instance.money -= myBuilding.money;
            StatisticManager.instance.food += myBuilding.food;
            growSpeed = fastSpeed;
            growSpeed /= 2;
        }
        else
        {
			StatisticManager.instance.money -= (myBuilding.money * 2);
            StatisticManager.instance.food += (myBuilding.food * 2);
            growSpeed = fastSpeed;
        }

        isGrowing = false;
        
        if (isGrowing == false)
        {
            isGrowing = true;
            if (wheatBool)
            {
                spawned = Instantiate(wheat, new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z), Quaternion.identity);
            }
            else
            {
                spawned = Instantiate(carrot, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
            }
        }
    }

    void Growing()
    {
        if (spawned != null)
        {

            spawned.transform.position = Vector3.MoveTowards(spawned.transform.position, targetLoc, growSpeed * Time.deltaTime);

            if (spawned.transform.position == targetLoc)
            {
                Harvest();
            }
        }
    }

    public void Harvest()
    {
        Destroy(spawned.gameObject);
        Spawning(wheatBool);
    }
}
