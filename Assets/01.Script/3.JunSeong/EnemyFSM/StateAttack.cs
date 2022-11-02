using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateAttack : State<EnemyFSM>
{
    private bool startAttack;

    public override void OnAwake()
    {

    }
    
    public override void OnStart()
    {
        Debug.Log("Start Attack");

        stateMachineClass.enemy.endAttack = false;
        startAttack = true;
    }

    public override void OnUpdate(float deltaTime)
    {
        
        if (startAttack)
        {
            startAttack = false;
            stateMachineClass.enemy.Attacking(stateMachineClass.target);
        }

        if (stateMachineClass.enemy.endAttack)
        {
            stateMachine.ChangeState<StateIdle>();
        }
    }

    public override void OnEnd()
    {
        Debug.Log("End Attack");
        stateMachineClass.enemy.endAttack = false;
        startAttack = false;
    }
}
