using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotSpeed;
    public float movSpeed;
    float horizontal;
    float vertical;
    float leftRot;
    float rightRot;

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal") * movSpeed * Time.deltaTime;
    }

}

