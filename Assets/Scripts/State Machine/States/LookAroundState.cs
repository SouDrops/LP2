using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAroundState : PlayerBaseState
{

    private readonly string PropertyName = "MovementSpeed";

    public LookAroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.inputActions.Player.Move.started += DetectMove;
    }

    public override void Exit()
    {
        stateMachine.inputActions.Player.Move.started -= DetectMove;
    }

    private void DetectMove(InputAction.CallbackContext context)
    {
        stateMachine.SwitchState(new MovePlayerState(stateMachine));
    }
    public override void Tick(float delta)
    {
        MoveCamera(Vector3.zero, delta);

        stateMachine.animator.SetInteger(PropertyName, (int)stateMachine.rigidbody.velocity.magnitude);

        stateMachine.MainCamera.transform.LookAt(stateMachine.LookPosition.position);   
    }
}
