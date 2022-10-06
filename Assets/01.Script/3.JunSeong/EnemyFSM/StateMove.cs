using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMove : State<EnemyFSM>
{
    NavMeshAgent nav;
    CharacterController characterController;

    public override void OnAwake()
    {
        nav = stateMachineClass.GetComponent<NavMeshAgent>();
        characterController = stateMachineClass.GetComponent<CharacterController>();
    }
    
    public override void OnStart()
    {
        Debug.Log("Start Move");
        stateMachineClass.animator.SetBool("Move", true);
    }

    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchPlayer();

        if(target)
        {
            nav.SetDestination(target.position);
            characterController.Move(nav.velocity.normalized * stateMachineClass.speed * Time.deltaTime);

            if(stateMachineClass.FlagAttack)
            {
                stateMachine.ChangeState<StateAttack>();
            }
        }
        else
        {
            nav.SetDestination(stateMachineClass.originPos);
            characterController.Move(nav.velocity.normalized * stateMachineClass.speed * Time.deltaTime);

            if(nav.remainingDistance <= nav.stoppingDistance)
            {
                stateMachine.ChangeState<StateIdle>();
            }
        }
    }

    public override void OnEnd()
    {
        Debug.Log("End Move");
        stateMachineClass.animator.SetBool("Move", false);
    }
}
