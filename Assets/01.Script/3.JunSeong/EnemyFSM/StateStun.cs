/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStun : State<EnemyFSM>
{
    private float currnetSpeed = 0f;
    
    public override void OnAwake()
    {

    }

    public override void OnStart()
    {
        currnetSpeed = stateMachineClass.speed;
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
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStun : State<EnemyFSM>
{
    public override void OnAwake()
    {

    }
    
    public override void OnStart()
    {

    }

    public override void OnUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnd()
    {
        throw new System.NotImplementedException();
    }
}

