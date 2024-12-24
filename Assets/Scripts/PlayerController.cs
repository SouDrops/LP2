using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float speed;

    public Rigidbody body;

    private PlayerControls inputActions;

    private Vector3 direction;

    public Transform playerEyes;

    public float rotationSpeed;

    private void Awake()
    {
        SetCameraPosition();

        inputActions = new PlayerControls();

        inputActions.Player.Move.started += Move_started;
        inputActions.Player.Move.performed += Move_started;
        inputActions.Player.Move.canceled += Move_started;

        inputActions.Player.Mouse.started += Mouse_started;
        inputActions.Enable();
    }

    private void Mouse_started(InputAction.CallbackContext obj)
    {
        Vector2 d = obj.ReadValue<Vector2>();

        d = d.normalized * rotationSpeed;

        transform.eulerAngles += new Vector3(0, d.x, 0);

        playerEyes.RotateAround(transform.position, Vector3.up, d.x);
        playerEyes.RotateAround(transform.position, Vector3.up, d.y);


    }

    private void OnDisable()
    {
        if (inputActions == null) return;

        inputActions.Player.Move.started -= Move_started;
        inputActions.Player.Move.performed -= Move_started;
        inputActions.Player.Move.canceled -= Move_started;

        inputActions.Enable();
    }

    private void Move_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 d = obj.ReadValue<Vector2>();

        direction = new Vector3(d.x, 0 ,d.y);
    }

    private void Update()
    {
        SetCameraPosition();
    }

    void FixedUpdate()
    {
        Vector2 input = inputActions.Player.Move.ReadValue<Vector2>();

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        direction = (forward * input.y + right * input.x).normalized;

        body.velocity = new Vector3(direction.x * speed, body.velocity.y, direction.z * speed);
    }


    private void SetCameraPosition()
    {
        Camera.main.transform.SetPositionAndRotation(playerEyes.position, playerEyes.rotation);
    }
}
