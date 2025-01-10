using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    public Animator animator;
    public Rigidbody rigidbody;

    [HideInInspector] public PlayerControls inputActions;
    [HideInInspector] public Camera MainCamera;

    public CameraInfo[] cameras;
    private GameObject currentCamera;

    public Transform LookPosition;

    [Header("Stats")]
    public float MovementSpeed;
    public float JumpForce = 10f;


    void Awake()
    {
        inputActions = new PlayerControls();
        inputActions.Enable();

        MainCamera = Camera.main;

        foreach (var c in cameras) 
            c.camera.SetActive(false);

        inputActions.Player.Jump.started += OnJumpStarted;

        SwitchState(new LookAroundState(this));
    }



    private void OnDisable()
    {
        inputActions.Player.Jump.started -= OnJumpStarted;
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Jump input detected.");

        if (currentState is JumpingState) return; // Prevent double jump

        // Check if the player is grounded before jumping
        if (currentState is LookAroundState || currentState is MovePlayerState)
        {
            SwitchState(new JumpingState(this)); // Switch to jumping state
        }
    }

    public void SwitchCamera(string name)
    {
        foreach (var c in cameras)
        {
            if (c.name == name)
            {
                currentCamera?.SetActive(false);
                currentCamera = c.camera;
                currentCamera?.SetActive(true);
                break;
            }
        }
    }
}

    [Serializable]
    public class CameraInfo
    {
        public string name;
        public GameObject camera;
    }

