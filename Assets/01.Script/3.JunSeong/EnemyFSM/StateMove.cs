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

        #region �� ���� �׷��� �׷� �׳� �� �׷� �ڵ����� �ٽ� ����� ���ɼ��� �ֱ� ������ ���� �������� �ʰ� �̷��� �ּ�ó���� �ϰ� �������� ������ �ڵ�
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
