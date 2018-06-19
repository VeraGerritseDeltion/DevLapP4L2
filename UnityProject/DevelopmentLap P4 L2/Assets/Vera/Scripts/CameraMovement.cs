using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float boundary;
    public float rotSpeed;
    public float movSpeed;

    public bool autoPanning = true;
    float screenX;
    float screenZ;

    float horizontal;
    float vertical;
    float rot;

    private void Start()
    {
        screenX = Screen.width;
        screenZ = Screen.height;
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Movement();
    }

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        rot = -Input.GetAxis("HorizontalRotation") * rotSpeed * Time.deltaTime / Time.timeScale;

        if (Input.GetButtonDown("Jump"))
        {
            Vector3 newPos = BuildingManager.instance.myTownHall.transform.position;
            newPos.y = 0;
            transform.position = newPos;
        }
        if (Input.GetButton("Fire3"))
        {
            rot = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime / Time.timeScale;
        }
        else if(autoPanning)
        {
           // if(Input.GetButton("Fire2"))
            if (Input.mousePosition.x < boundary)
            {
                horizontal = -1;
            }
            if (Input.mousePosition.x > screenX - boundary)
            {
                horizontal = 1;
            }
            if (Input.mousePosition.y < boundary)
            {
                vertical = -1;
            }
            if (Input.mousePosition.y > screenZ - boundary)
            {
                vertical = 1;
            }
        }
        transform.Rotate(new Vector3(0, rot, 0));
        horizontal = horizontal * movSpeed * Time.deltaTime / Time.timeScale;
        vertical = vertical * movSpeed * Time.deltaTime / Time.timeScale;
        transform.Translate(new Vector3(horizontal, 0, vertical));
    }

}

