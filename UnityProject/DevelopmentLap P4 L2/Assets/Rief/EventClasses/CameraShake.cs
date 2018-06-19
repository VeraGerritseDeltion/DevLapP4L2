using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour{

    public float power;
    public float duration;
    public Transform myCamera;
    public bool shouldShake;

    Vector3 startPosition;
    float initialDuration;
    List<Coroutine> allTimers = new List<Coroutine>();

    void Start()
    {
        myCamera = Camera.main.transform;
        startPosition = myCamera.localPosition;
        initialDuration = duration;
    }

    void Update()
    {
        if (shouldShake)
        {
           allTimers.Add(StartCoroutine(IsShaking()));
        }
    }
    IEnumerator IsShaking()
    {
        startPosition = myCamera.localPosition;
        myCamera.localPosition = startPosition + Random.insideUnitSphere * power;
        yield return new WaitForSeconds(duration);
        shouldShake = false;
        for (int i = 0; i < allTimers.Count; i++)
        {
            StopCoroutine(allTimers[i]);
        }
        duration = initialDuration;
        myCamera.localPosition = startPosition;
    }
}
