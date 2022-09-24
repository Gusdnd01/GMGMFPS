using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State<EnemyFSM>
{
    private CharacterController characterController;

    public override void OnAwake()
    {
        characterController = stateMachineClass.GetComponent<CharacterController>();
    }

    public override void OnStart()
    {
        characterController.Move(Vector3.zero);
        stateMachineClass.speed = stateMachineClass.data.walkSpeed;
    }

    public override void OnUpdate(float deltaTime)
    {
        if(!stateMachineClass.isHit)
        {
            UnhitProcess();
        }
        else
        {
            HitProcess();
        }
    }

    public override void OnEnd()
    {
        Debug.Log("End Idle");
    }

    public override void OnHitEvent()
    {

    }

    private void UnhitProcess()
    {
        Transform target = stateMachineClass.SearchPlayer();

        if(target)
        {
            if(stateMachineClass.GetFlagAttack)
            {
                stateMachine.ChangeState<StateAttack>();
            }
            else
            {
                stateMachine.ChangeState<StateMove>();
            }
        }
    }

    private void HitProcess()
    {
        stateMachine.ChangeState<StateMove>();
    }
}