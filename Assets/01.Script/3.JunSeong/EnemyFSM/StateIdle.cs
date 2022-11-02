using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State<EnemyFSM>
{
    private Animator animator;

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
    }
    
    public override void OnStart()
    {
        Debug.Log("Start Idle");

        animator.SetFloat(stateMachineClass.moveSpeedHash, 0f);
    }

    public override void OnUpdate(float deltaTime)
    {
        if(stateMachineClass.idleTime <= deltaTime)
        {
            stateMachine.ChangeState<StateMove>();
        }
    }

    public override void OnEnd()
    {
        Debug.Log("End Idle");
    }
}
