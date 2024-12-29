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
    public AudioSource audioSource;

    [HideInInspector] public Camera MainCamera;
    [HideInInspector] public PlayerControls inputActions;

    //public Transform LookPosition;

    [Header("Stats")]
    public float MovementSpeed;
    public float JumpForce = 10f;

    public Transform followTarget;
    public NavMeshAgent navMeshAgent; 
    public NavMeshSurface navMeshSurface;
    public NavMeshData navMeshData;
    void Awake()
    {
        //inputActions = new PlayerControls();
        //inputActions.Enable();

        //inputActions.Player.Jump.started += OnJumpStarted;

        //MainCamera = Camera.main;

        //SwitchState(new LookAroundState(this));
    }

    private void Update()
    {
       navMeshAgent.destination = followTarget.position;
       navMeshSurface.UpdateNavMesh(navMeshData);
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
