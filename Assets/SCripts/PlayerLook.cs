using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;

    private float xRotation = 0f;

    public float xSensitivity = 30f;

    public float ySensitivity = 30f;
    
    public float rotationDamping = 10f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
    
        // Calculate rotation of the camera
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
    
        // Smoothly apply rotation to the camera using Quaternion.Slerp
        Quaternion desiredRotation = Quaternion.Euler(xRotation, 0, 0);
        cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, desiredRotation, Time.deltaTime * rotationDamping);
    
        // Rotate the player
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
