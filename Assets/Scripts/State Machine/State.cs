using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    public abstract void Enter();

    public abstract void Tick(float delta);
  
    public abstract void Exit();  
}
