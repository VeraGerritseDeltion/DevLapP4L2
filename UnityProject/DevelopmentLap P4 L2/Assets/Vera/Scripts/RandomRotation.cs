using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
	void Start ()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, Random.Range(0, 360), transform.rotation.z));
	}
}
