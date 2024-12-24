using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerBaseState
{
    public JumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        // Ensure the player is grounded before applying the jump force
        if (IsGrounded())
        {
            // Apply jump force
            stateMachine.rigidbody.AddForce(Vector3.up * stateMachine.JumpForce, ForceMode.Impulse);
            stateMachine.animator.SetTrigger("IsJumping");
        }
    }

    public override void Exit()
    {
        stateMachine.animator.ResetTrigger("IsJumping");
    }

    public override void Tick(float delta)
    {
        if (stateMachine.rigidbody.velocity.y <= 0f && IsGrounded())
        {
            stateMachine.SwitchState(new LookAroundState(stateMachine)); // Switch to a grounded state
        }
    }

    private bool IsGrounded()
    {
        // Using a smaller raycast distance to check if the player is near the ground
        float raycastDistance = 0.2f;
        Vector3 origin = stateMachine.transform.position; /*+ Vector3.down * (stateMachine.GetComponent<CapsuleCollider>().bounds.extents.y + 0.1f);*/

        // Perform the raycast to check if the player is grounded
        return Physics.Raycast(origin, Vector3.down, raycastDistance);
    }
}
