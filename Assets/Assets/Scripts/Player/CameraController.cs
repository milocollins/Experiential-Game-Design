using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController firstPerson;
    private bool inputLock = false;
    private float xRotation = 0f;
    public float defaultXSensitivity = 100f;
    public float defaultYSensitivity = 100f;
    public float zoomXSensitivity = 100f;
    public float zoomYSensitivity = 100f;
    private float xSensitivity;
    private float ySensitivity;
    public Transform playerBody;
    public float minXClamp = -60f;
    public float maxXClamp = 60f;
    public float defaultFov = 60f;
    public float zoomFov = 20f;
    public float lookAtSmooth;
    private void Awake()
    {
        firstPerson = this;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetFOV(defaultFov);
        UpdateSensitivty(defaultXSensitivity, defaultYSensitivity);
        xRotation = 0f;
    }
    public void ToggleInput()
    {
        inputLock = !inputLock;
    }

    void Update()
    {
        if (!inputLock)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, PlayerController.thePlayer.raycastLength, PlayerController.thePlayer.interactMask))
            {
                if (hit.transform.GetComponent<Tooltip>())
                {
                    if (!UIManager.ui.tooltipBool)
                    {
                        hit.transform.GetComponent<Tooltip>().SetActive();
                    }
                }
            }
            else
            {
                if (UIManager.ui.tooltipBool)
                {
                    UIManager.ui.ResetTooltip();
                }
            }
            if (Input.GetButtonDown("Zoom"))
            {
                SetFOV(zoomFov);
                UpdateSensitivty(zoomXSensitivity, zoomYSensitivity);
            }
            if (Input.GetButtonUp("Zoom"))
            {
                SetFOV(defaultFov);
                UpdateSensitivty(defaultXSensitivity, defaultYSensitivity);
            }
            float mouseX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minXClamp, maxXClamp);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(playerBody.up * mouseX);
        }
    }

    private void UpdateSensitivty(float x, float y)
    {
        xSensitivity = x;
        ySensitivity = y;
    }

    public void SetFOV(float fov)
    {
        transform.GetComponent<Camera>().fieldOfView = fov;
    }
}
