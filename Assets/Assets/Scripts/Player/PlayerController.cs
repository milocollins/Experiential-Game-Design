using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController thePlayer;
    private CharacterController myController;
    private bool inputLock = false;
    public Camera dialogueCamera;
    public CharacterProfile myProfile;
    [Header ("Movement")]
    public float walkSpeed;
    public float crouchWalkSpeed;
    public float runSpeed;
    public float crouchRunSpeed;
    private float currentSpeed;
    private bool isRunning = false;
    private bool isRecovering = false;
    public float maxStamina = 40f;
    public float stamina;
    public float staminaLoss;
    public float staminaRecovery;
    [Header ("Character Controller Settings")]
    public float defaultHeight;
    public float crouchHeight;
    private Vector3 velocity = Vector3.zero;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float gravity = 9.81f;
    private bool isGrounded = false;
    public float groundedDistance = 0.4f;
    private bool isCrouched = false;
    public Transform raycastSource;
    public float raycastLength;
    public LayerMask interactMask;
    private void Awake()
    {
        thePlayer = this;
    }
    private void Start()
    {
        myController = gameObject.GetComponent<CharacterController>();
        SetSpeed(walkSpeed);
        stamina = maxStamina;
    }
    private void Update()
    {
        if (!inputLock)
        {
            if (Input.GetButtonDown("Crouch"))
            {
                CrouchToggle();
            }
            if (isRecovering)
            {
                stamina += staminaRecovery * Time.deltaTime;
                if (stamina >= maxStamina)
                {
                    stamina = maxStamina;
                    isRecovering = false;
                }
            }
            if (Input.GetButton("Run"))
            {
                if (!isRecovering)
                {
                    if (!isRunning)
                    {
                        RunToggle();
                    }
                    stamina -= staminaLoss * Time.deltaTime;
                    if (stamina <= 0)
                    {
                        isRecovering = true;
                        RunToggle();
                    }
                }
            }
            if (Input.GetButtonUp("Run"))
            {
                if (isRunning)
                {
                    RunToggle();
                }
            }
            if (Input.GetButtonDown("Interact"))
            {
                Debug.DrawLine(raycastSource.position, raycastSource.position + raycastSource.forward * raycastLength, Color.red, 1f);
                RaycastHit hit;
                if (Physics.Raycast(raycastSource.position, raycastSource.forward, out hit, raycastLength, interactMask))
                {
                    Utilities.Interact(hit.transform);
                }
                else
                {
                    Debug.Log("NO HIT");
                }
            }
            Movement();
        }
    }
    public void ToggleInput()
    {
        inputLock = !inputLock;
    }
    private void RunToggle()
    {
        isRunning = !isRunning;
        if (isRunning)
        {
            if (isCrouched)
            {
                currentSpeed = crouchRunSpeed;
            }
            else
            {
                currentSpeed = runSpeed;
            }
        }
        else
        {
            if (isCrouched)
            {
                currentSpeed = crouchWalkSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }
        }
    }
    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        isGrounded = GroundedCheck();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        myController.Move(move * currentSpeed * Time.deltaTime);

        velocity.y -= gravity * Time.deltaTime;
        myController.Move(velocity * Time.deltaTime);

    }
    public void SetSpeed(float s)
    {
        currentSpeed = s;
    }
    private bool GroundedCheck()
    {
        if (Physics.CheckSphere(groundCheck.position, groundedDistance, groundMask))
        {
            return true;
        }
        return false;
    }
    private void CrouchToggle()
    {
        isCrouched = !isCrouched;
        if (isCrouched)
        {
            myController.height = crouchHeight;
            SetSpeed(crouchWalkSpeed);
        }
        else
        {
            myController.height = defaultHeight;
            SetSpeed(walkSpeed);
        }
    }
}
