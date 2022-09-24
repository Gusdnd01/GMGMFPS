using System;
using System.Collections.Generic;

public sealed class StateMachine<T>
{
    private T stateMachine;
    public Dictionary<Type, State<T>> stateList = new Dictionary<Type, State<T>>();

    private State<T> nowState;
    public State<T> NowState => nowState;

    private State<T> beforeState;
    public State<T>  BeforeState => beforeState;

    private float stateDurationTime = 0f;
    public float StateDurationTime => stateDurationTime;

    public StateMachine(T _stateMachine, State<T> initState)
    {
        this.stateMachine = _stateMachine;
        AddStateList(initState);
        nowState = initState;
        nowState.OnStart();
    }

    public void AddStateList(State<T> state)
    {
        state.SetStateMachineWithClass(this, stateMachine);
        state.OnAwake();
        stateList[state.GetType()] = state;
    }

    public Q ChangeState<Q>() where Q : State<T>
    {
        var newState = typeof(Q);
        
        if(nowState.GetType() == newState)
        {
            return nowState as Q;
        }

        if(nowState.GetType() != newState)
        {
            nowState.OnEnd();
        }

        beforeState = nowState;
        nowState = stateList[newState];

        nowState.OnStart();
        stateDurationTime = 0f;

        return nowState as Q;
    }

    public void Update(float deltaTime) 
    {
        stateDurationTime += deltaTime;
        nowState.OnUpdate(StateDurationTime);
    }

    public void OnHitEvent()
    {
        nowState.OnHitEvent();
    }
}
