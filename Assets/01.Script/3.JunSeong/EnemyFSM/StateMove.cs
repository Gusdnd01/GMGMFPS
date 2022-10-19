using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMove : State<EnemyFSM>
{
    NavMeshAgent nav;
    CharacterController characterController;
    Boss boss;

    public override void OnAwake()
    {
        nav = stateMachineClass.GetComponent<NavMeshAgent>();
        characterController = stateMachineClass.GetComponent<CharacterController>();
        boss = stateMachineClass.boss;
    }
    
    public override void OnStart()
    {
        Debug.Log("Start Move");
        stateMachineClass.animator.SetBool("Move", true);
    }

    public override void OnUpdate(float deltaTime)
    {
        if(boss.CheckAngle())
        {
            boss.Move();
        }
        else
        {
            boss.Turn();
        }

        if(deltaTime >= 3f || stateMachineClass.FlagAttack)
        {
            stateMachine.ChangeState<StateAttack>();
        }
    }

    public override void OnEnd()
    {
        Debug.Log("End Move");
        stateMachineClass.animator.SetBool("Move", false);
    }
}
