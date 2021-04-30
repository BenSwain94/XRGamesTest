using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform Target, Player;
    float mouseX, mouseY;

    // Update is called once per frame
    void LateUpdate()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
