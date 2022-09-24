using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public enum enemyState
    {
        Idle,
        Walk,
        Run,
        Attack,
        Stun,
        Die
    }
    public enemyState state = enemyState.Idle;

    [SerializeField] protected EnemyData data;

    [SerializeField] protected bool isAttack = false;
    [SerializeField] protected bool isStun = false;

    protected enemyState lastState;

    [SerializeField] protected int hp;

    protected Transform playerTrm;
    protected NavMeshAgent nav;

    private void Awake()
    {
        playerTrm = GameObject.Find("Player").GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();

        hp = data.hp;
    }

    protected IEnumerator FSMCycle()
    {
        while (state != enemyState.Die)
        {
            switch (state)
            {
                case enemyState.Idle:
                    Idle();
                    break;
                case enemyState.Walk:
                    Walk();
                    break;
                case enemyState.Run:
                    Run();
                    break;
                case enemyState.Attack:
                    Attack();
                    break;
                case enemyState.Stun:
                    Stun();
                    break;
            }

            yield return new WaitForSeconds(0.1f);
        }

        OnDie();
    }

    protected abstract void Idle();
    protected abstract void Walk();
    protected abstract void Run();
    protected abstract void Stun();
    protected abstract void Attack();
    protected abstract void OnDie();
    public abstract void OnDamaged(int damage);

    protected float GetDistance()
    {
        return (playerTrm.position - transform.position).magnitude;
    }
}
