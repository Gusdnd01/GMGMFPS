using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateAttack : State<EnemyFSM>
{
    NavMeshAgent nav;
    CharacterController characterController;
    Boss boss;

    private bool endAttack = false;
    private bool startAttack = true;

    public override void OnAwake()
    {
        boss = stateMachineClass.boss;
    }
    
    public override void OnStart()
    {
        Debug.Log("Start Attack");

        if(startAttack)
        {
            startAttack = false;
            boss.Attacking();
        }

        if(endAttack)
        {
            stateMachine.ChangeState<StateMove>();
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        boss.Attacking();
    }

    public override void OnEnd()
    {
        Debug.Log("End Attack");
        endAttack = false;
        startAttack = false;
    }

    public void EndAttack()
    {
        endAttack = true;
    }
}
