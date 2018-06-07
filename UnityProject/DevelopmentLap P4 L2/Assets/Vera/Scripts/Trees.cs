﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour {

    public Renderer myRend;
    public Material myMat;
    public float myRand;
    ParticleSystem fire;
    float burnt;
    Color burntColor;
    Color notBurnedColor;
    public float burnSpeed;
    Color myColor;
    public float minTime;
    public float maxTime;
    public float radius;
    public LayerMask trees;
    List<Coroutine> allRoutines = new List<Coroutine>();

    int burning;
	void Start () {
        NatureManager.instance.allTrees.Add(gameObject);
        myRend = GetComponentInChildren<Renderer>();
        myMat = myRend.material;
        fire = GetComponentInChildren<ParticleSystem>();
        float rand = Random.Range(0, 100);
        myRand = rand / 100;
        burnSpeed = 0.1f;
        transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        transform.position = Ground();
    }

    public Vector3 Ground()
    {
        RaycastHit hit;
        Physics.Raycast(gameObject.transform.position, Vector3.down, out hit,NatureManager.instance.ground);
        return hit.point;
    }

    public void ChangeColor(Color myNewColor)
    {
        if (burnt == 0)
        {
            myMat.color = myNewColor;
        }
    }

	void Update () {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartFire();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            EndFire();
        }
        Debug.DrawRay(gameObject.transform.position, Vector3.down, Color.red);
    }
    public void StartFire()
    {
        burning = 1;
        fire.Play();
        allRoutines.Add(StartCoroutine(BurnTrees()));
        allRoutines.Add(StartCoroutine(BurnTreeInFacainity()));
    }
    public void EndFire()
    {
        burning = -1;
        for (int i = 0; i < allRoutines.Count; i++)
        {
            StopCoroutine(allRoutines[i]);
        }
        allRoutines.Clear();
        fire.Stop();
        allRoutines.Add(StartCoroutine(BurnTrees()));
    }

    IEnumerator BurnTrees()
    {
        yield return new WaitForSeconds(burnSpeed);
        burnt += burning;
        if(burnt >= 100)
        {
            burnt = 100;
        }
        if(burnt<= 0)
        {
            burnt = 0;
        }

        myMat.color = Color.Lerp(Color.Lerp(NatureManager.instance.currentLow, NatureManager.instance.currentHigh, myRand), Color.Lerp(NatureManager.instance.burnColorLow, NatureManager.instance.burnColorHigh, myRand), burnt/100);
        if(burnt == 100)
        {
            StartCoroutine(StopFire());
        }
        else
        {
            allRoutines.Add(StartCoroutine(BurnTrees()));
        }
    }

    IEnumerator StopFire()
    {
        yield return new WaitForSeconds(Random.Range(3, 7));
        burning = 0;
        fire.Stop();
    }

    IEnumerator BurnTreeInFacainity()
    {
        float rand = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(rand / 10);
        if (Random.Range(0, 100) < 50)
        {
            Collider[] facinity = Physics.OverlapSphere(transform.position, radius, trees);
            print(facinity.Length);
            int randa = Random.Range(0, facinity.Length);
            if(facinity.Length != 0)
            {
                if(facinity[randa].GetComponent<Trees>().burning != 1 && facinity[randa].GetComponent<Trees>().burnt != 100)
                {
                    facinity[randa].GetComponent<Trees>().StartFire();
                }
            }
        }
        if (burning == 1)
        {
            allRoutines.Add(StartCoroutine(BurnTreeInFacainity()));
        }
    }
}
