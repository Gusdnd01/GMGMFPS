using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

public class CameraMove : MonoBehaviour
{
    public float m_sensitivity = 1;

    float xRotation;

    [SerializeField]
    Transform player;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MouseControll();
    }
    private void MouseControll()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        transform.localRotation = Quaternion.Euler(xRotation * m_sensitivity, 0, 0);

        player.Rotate(0, mouseX * m_sensitivity, 0);
    }
}
