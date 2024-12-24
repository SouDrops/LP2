using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    public PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    { this.stateMachine = stateMachine; }

    public void MoveCamera(Vector3 move, float deltaTime)
    {
        Camera.main.transform.position += move * deltaTime;
    }
}
