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
            StartCoroutine(IsShaking());
        }
    }
    IEnumerator IsShaking()
    {
        myCamera.localPosition = startPosition + Random.insideUnitSphere * power;
        yield return new WaitForSeconds(duration);
        shouldShake = false;
        duration = initialDuration;
        myCamera.localPosition = startPosition;
    }
}
