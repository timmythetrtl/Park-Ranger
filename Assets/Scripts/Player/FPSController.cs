using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera; // Change the type to CinemachineVirtualCamera
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private PlayerInput playerInput; 
    private Transform playerTransform; // Store the player's transform separately

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerTransform = transform; // Cache the player's transform

        // Get the reference to the Player Input component attached to the player object
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        #region Handles Movement
        Vector2 movementInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        Vector3 forward = playerTransform.TransformDirection(Vector3.forward); // Use the cached player's transform
        Vector3 right = playerTransform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * movementInput.y : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * movementInput.x : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        #endregion

        #region Handles Jumping
        if (playerInput.actions["Jump"].triggered && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        // Inside the Update method
if (canMove && Time.timeScale > 0f && playerCamera != null)
{
    CinemachineOrbitalTransposer orbitalTransposer = playerCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

    if (orbitalTransposer != null)
    {
        orbitalTransposer.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
        orbitalTransposer.m_XAxis.Value = rotationX;
    }

    rotationX += -playerInput.actions["Look"].ReadValue<Vector2>().y * lookSpeed;
    rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

    playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    transform.rotation *= Quaternion.Euler(0, playerInput.actions["Look"].ReadValue<Vector2>().x * lookSpeed, 0);
}

        #endregion
    }
}
