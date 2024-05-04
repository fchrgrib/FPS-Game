using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{

    public Camera Camera;
    public GameObject pet;
    private float vRotation = 0f;

    public float hSensitivity = 30f;
    public float vSensitivity = 30f;

    public void MoveDirection(Vector2 input)
    {
        float mouseX = input.x, mouseY = input.y;

        vRotation -= (mouseY * Time.deltaTime) * vSensitivity;
        vRotation = Mathf.Clamp(vRotation, -80f, 80f);
        Camera.transform.localRotation = Quaternion.Euler(vRotation, 0, 0);
        transform.Rotate(mouseX * Time.deltaTime * hSensitivity * Vector3.up);
        // pet?.transform.Rotate(mouseX * Time.deltaTime * hSensitivity * Vector3.up);
    }
}