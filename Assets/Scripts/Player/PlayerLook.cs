using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //rotate the player based on the mouse input
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //rotate the camera based on the mouse input
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //rotate the player based on the mouse input
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

}