using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    protected StateMachine<T> stateMachine;
    protected T stateMachineClass;

    public void SetStateMachineWithClass(StateMachine<T> _stateMachine,T _stateMachineClass)
    {
        stateMachine = _stateMachine;
        stateMachineClass = _stateMachineClass;
    }

    public abstract void OnAwake();
    public abstract void OnStart();
    public abstract void OnUpdate(float deltaTime);
    public abstract void OnEnd();
    public virtual void OnHit() {}
}
