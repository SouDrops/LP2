using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    public Animator animator;
    public Rigidbody rigidbody;
    public AudioSource audioSource;

    [HideInInspector] public Camera MainCamera;
    [HideInInspector] public PlayerControls inputActions;

    public Transform LookPosition;

    [Header("Stats")]
    public float MovementSpeed;
    public float JumpForce = 10f;

    void Awake()
    {
        inputActions = new PlayerControls();
        inputActions.Enable();

        inputActions.Player.Jump.started += OnJumpStarted;

        MainCamera = Camera.main;

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
}
