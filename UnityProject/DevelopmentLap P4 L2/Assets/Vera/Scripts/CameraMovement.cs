using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotSpeed;
    public float movSpeed;
    float horizontal;
    float vertical;
    float rot;

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal") * movSpeed * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * movSpeed * Time.deltaTime;

        transform.Translate(new Vector3(horizontal, 0, vertical));

        rot = Input.GetAxis("HorizontalRotation") * rotSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, rot, 0));
    }

}

