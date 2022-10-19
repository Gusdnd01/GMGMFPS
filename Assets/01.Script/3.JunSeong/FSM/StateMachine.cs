using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StateMachine<T>
{
    private T stateMachineClass;
    private Dictionary<Type, State<T>> stateList = new Dictionary<Type, State<T>>();

    private State<T> beforeState;
    public State<T> BeforeState => beforeState;

    private State<T> nowState;
    public State<T> NowState => nowState;

    private float stateDurationTime;
    public float StateDurationTime => stateDurationTime;

    public StateMachine(T _stateMachineClass, State<T> initState)
    {
        stateMachineClass = _stateMachineClass;
        AddState(initState);
        nowState = initState;
        nowState.OnStart();
    }

    public void AddState(State<T> state)
    {
        state.SetStateMachineWithClass(this, stateMachineClass);
        stateList[state.GetType()] = state;
        state.OnAwake();
    }

    public void ChangeState<Q>() where Q : State<T>
    {
        Type newState = typeof(Q);

        if(newState == nowState.GetType())
        {
            return;
        }
        else
        {
            nowState.OnEnd();
            beforeState = nowState;
            nowState = stateList[newState];
            nowState.OnStart();

            stateDurationTime = 0;
        }
    }

    public void Update(float deltaTime)
    {   
        stateDurationTime += deltaTime;
        nowState.OnUpdate(stateDurationTime);
    }

    public void OnHit()
    {
        nowState.OnHit();
    }
}
