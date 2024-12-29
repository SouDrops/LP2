using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float speed;

    public Rigidbody body;

    private PlayerControls inputActions;

    private Vector3 direction;

    public Transform playerEyes;

    public float rotationSpeed;

    public GameObject ragdollRoot; // Reference to the ragdoll root
    
    private bool isRagdollActive = false;

    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent
    private Animator animator; // Reference to the Animator


    public void ActivateRagdoll()
    {
        if (isRagdollActive) return; // Prevent double activation
        isRagdollActive = true;

        // Stop the NavMeshAgent
        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.enabled = false; // Disable completely
        }

        // Disable Animator to stop animations
        if (animator != null)
        {
            animator.enabled = false;
        }

        // Enable ragdoll physics
        Rigidbody[] ragdollBodies = ragdollRoot.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in ragdollBodies)
        {
            body.isKinematic = false; // Enable physics simulation
            body.detectCollisions = true; // Allow collisions
        }
    }

    private void Awake()
    {
        //SetCameraPosition();

        inputActions = new PlayerControls();

        inputActions.Player.Move.started += Move_started;
        inputActions.Player.Move.performed += Move_started;
        inputActions.Player.Move.canceled += Move_started;

        //inputActions.Player.Mouse.started += Mouse_started;
        inputActions.Enable();

        // Cache components
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    //private void Mouse_started(InputAction.CallbackContext obj)
    //{
    //    Vector2 d = obj.ReadValue<Vector2>();

    //    d = d.normalized * rotationSpeed;

    //    transform.eulerAngles += new Vector3(0, d.x, 0);

    //    playerEyes.RotateAround(transform.position, Vector3.up, d.x);
    //    playerEyes.RotateAround(transform.position, Vector3.up, d.y);


    //}

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

    
    void FixedUpdate()
    {
        Vector2 input = inputActions.Player.Move.ReadValue<Vector2>();

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        direction = (forward * input.y + right * input.x).normalized;

        body.velocity = new Vector3(direction.x * speed, body.velocity.y, direction.z * speed);
    }


    //private void SetCameraPosition()
    //{
    //    Camera.main.transform.SetPositionAndRotation(playerEyes.position, playerEyes.rotation);
    //}
}
