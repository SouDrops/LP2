using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAroundState : PlayerBaseState
{
    public LookAroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        PropertyName = "Movement Speed";
        CameraName = "Look";
    }

    public override void Enter()
    {
        stateMachine.inputActions.Player.Move.started += DetectMove;

        stateMachine.SwitchCamera(CameraName);
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
        stateMachine.animator.SetInteger(PropertyName, (int)stateMachine.rigidbody.velocity.magnitude);
    }
}
