using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStun : State<EnemyFSM>
{
    private float currnetSpeed = 0f;
    private State<EnemyFSM> beforeState;

    public override void OnAwake()
    {

    }

    public override void OnStart()
    {
        currnetSpeed = stateMachineClass.speed;
        beforeState = stateMachine.BeforeState;
    }

    public override void OnUpdate(float deltaTime)
    {
        
        stateMachineClass.speed = 0;

        if(stateMachine.StateDurationTime >= stateMachineClass.data.stunTime)
        {
            stateMachineClass.speed = currnetSpeed;
            stateMachine.ChangeState<StateMove>();
        }
    }

    public override void OnEnd()
    {
        Debug.Log("Stun End");
    }

    public override void OnHitEvent()
    {
        
    }
}
