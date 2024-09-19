using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float sprintSpeed = 10.0f;
    [SerializeField] private float gravity = 9.81f;

    private CharacterController controller;
    private Vector3 moveDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 direction = GetInputDirection();
        float currentSpeed = GetCurrentSpeed();

        moveDirection.x = direction.x * currentSpeed;
        moveDirection.z = direction.z * currentSpeed;

        ApplyGravity();

        controller.Move(moveDirection * Time.deltaTime);
    }

    private Vector3 GetInputDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        return transform.forward * verticalInput + transform.right * horizontalInput;
    }

    private float GetCurrentSpeed()
    {
        return Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }
}