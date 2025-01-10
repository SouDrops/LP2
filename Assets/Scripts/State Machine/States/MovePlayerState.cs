using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayerState : PlayerBaseState
{
    private Vector3 input;
    public MovePlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        PropertyName = "Movement Speed";
        CameraName = "Move";
    }
    
    public override void Enter()
    {
       stateMachine.inputActions.Player.Move.started += Move_started;
       stateMachine.inputActions.Player.Move.performed += Move_started;
       stateMachine.inputActions.Player.Move.canceled += Move_started;

       stateMachine.SwitchCamera(CameraName);
    }

    private void Move_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 d = obj.ReadValue<Vector2>();

        input = new Vector3(d.x, 0, d.y);

        if (d.x == 0f && d.y == 0f)
            stateMachine.SwitchState(new LookAroundState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.rigidbody.velocity = Vector3.zero;

        stateMachine.inputActions.Player.Move.started -= Move_started;
        stateMachine.inputActions.Player.Move.performed -= Move_started;
        stateMachine.inputActions.Player.Move.canceled -= Move_started;
    }

    public override void Tick(float delta)
    {
        Vector3 fwd = stateMachine.gameObject.transform.forward;
        Vector3 rgt = stateMachine.gameObject.transform.right;

        stateMachine.GetComponent<Rigidbody>().velocity = new Vector3(rgt.x * input.x, 0 ,fwd.z * input.z) * stateMachine.MovementSpeed;

        stateMachine.animator.SetInteger(PropertyName, (int)stateMachine.GetComponent<Rigidbody>().velocity.magnitude);

        RotateObject();
    }

    private void RotateObject()
    {
       Vector2 delta = stateMachine.inputActions.Player.Rotation.ReadValue<Vector2>();

        stateMachine.gameObject.transform.Rotate(Vector2.up * delta.x);
    }
}
