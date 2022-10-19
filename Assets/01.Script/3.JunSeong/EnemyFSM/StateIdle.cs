using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State<EnemyFSM>
{ 

    public override void OnAwake()
    {
        
    }
    
    public override void OnStart()
    {
        Debug.Log("Start Idle");
    }

    public override void OnUpdate(float deltaTime)
    {
        stateMachine.ChangeState<StateMove>();
    }

    public override void OnEnd()
    {
        Debug.Log("End Idle");
    }
}
