using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    public PlayerStateMachine stateMachine;

    public string PropertyName;
    public string CameraName;
    public PlayerBaseState(PlayerStateMachine stateMachine)
    { this.stateMachine = stateMachine; }
}
