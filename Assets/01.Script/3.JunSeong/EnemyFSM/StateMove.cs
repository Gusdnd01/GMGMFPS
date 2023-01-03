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

    private Transform _player = null;
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return _player;
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        stateMachineClass.animator.SetFloat(stateMachineClass.moveSpeedHash, Mathf.Lerp(stateMachineClass.moveSpeedHash,
            stateMachineClass.enemy.MoveSpeed, deltaTime * speedChangeAmount));

        #region 음 뭔가 그렇고 그런 그냥 좀 그런 코드지만 다시 사용할 가능성이 있기 때문에 굳이 지워주지 않고 이렇게 주석처리를 하고 리젼으로 묶어준 코드
        /*if(stateMachineClass.enemy.CheckAngle())
        {
            stateMachineClass.enemy.Move();
            Debug.Log("Move");
        }
        else
        {
            stateMachineClass.enemy.Turn();
            Debug.Log("Turn");
        }*/
        #endregion

        if (Vector3.Distance(stateMachineClass.transform.position, Player.position) < 50)
        stateMachineClass.enemy.Move();
        Debug.Log("Move");

        if (stateMachineClass.moveTime <= deltaTime || stateMachineClass.SearchPlayer() 
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
