using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMove : State<EnemyFSM>
{
    private float speedChangeAmount = 10;

    public override void OnAwake()
    {
        
    }
    
    public override void OnStart()
    {
        Debug.Log("Start Move");
    }

    public override void OnUpdate(float deltaTime)
    {
        stateMachineClass.animator.SetFloat(stateMachineClass.moveSpeedHash, Mathf.Lerp(stateMachineClass.moveSpeedHash,
            stateMachineClass.enemy.MoveSpeed, deltaTime * speedChangeAmount));

        if(stateMachineClass.enemy.CheckAngle())
        {
            stateMachineClass.enemy.Move();
            Debug.Log(stateMachineClass.enemy.CheckAngle());
            Debug.Log("Move");
        }
        else
        {
            stateMachineClass.enemy.Turn();
            Debug.Log("Turn");
        }

        if(stateMachineClass.moveTime <= deltaTime || stateMachineClass.SearchPlayer() 
        || (stateMachineClass.FlagAttack && stateMachineClass.enemy.CheckAngle()))
        {
            stateMachine.ChangeState<StateAttack>();
        }
    }

    public override void OnEnd()
    {
        Debug.Log("End Move");
    }
}
