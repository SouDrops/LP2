using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayerState : PlayerBaseState
{
    private Vector3 direction;

    private readonly string PropertyName = "MovementSpeed";

    public MovePlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
       stateMachine.inputActions.Player.Move.started += Move_started;
       stateMachine.inputActions.Player.Move.performed += Move_started;
       stateMachine.inputActions.Player.Move.canceled += Move_started;
    }

    private void Move_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 d = obj.ReadValue<Vector2>();

        direction = new Vector3(d.x, 0, d.y);

        if (d.x == 0f && d.y == 0f)
            stateMachine.SwitchState(new LookAroundState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.rigidbody.velocity = Vector3.zero;

        stateMachine.audioSource.Stop();

        stateMachine.inputActions.Player.Move.started -= Move_started;
        stateMachine.inputActions.Player.Move.performed -= Move_started;
        stateMachine.inputActions.Player.Move.canceled -= Move_started;
    }

    public override void Tick(float delta)
    {
        stateMachine.rigidbody.velocity = direction * stateMachine.MovementSpeed;

        stateMachine.animator.SetInteger(PropertyName, (int)stateMachine.rigidbody.velocity.magnitude);

        if(!stateMachine.audioSource.isPlaying) 
            stateMachine.audioSource.Play();

        MoveCamera(direction * stateMachine.MovementSpeed, delta);
    }
}
