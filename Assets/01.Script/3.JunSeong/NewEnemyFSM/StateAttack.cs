using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateAttack : State<EnemyFSM>
{
    NavMeshAgent nav;
    CharacterController characterController;
    AttackPattern attackPattern;

    public override void OnAwake()
    {
        attackPattern = stateMachineClass.GetComponent<AttackPattern>();
    }
    
    public override void OnStart()
    {
        Debug.Log("Start Attack");
    }

    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchPlayer();

        if(target)
        {
            attackPattern.Attack(target);
        }
        else
        {
            stateMachine.ChangeState<StateMove>();
        }
        
    }

    public override void OnEnd()
    {
        Debug.Log("End Attack");
    }
}
