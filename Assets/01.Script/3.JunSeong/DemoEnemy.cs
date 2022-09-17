using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnemy : Enemy
{
    private void Start()
    {
        StartCoroutine(FSMCycle());
    }

    protected override void Idle()
    {
        Debug.Log("Idle");

        if (GetDistance() <= data.activeDistance)
        {
            nav.SetDestination(playerTrm.position);
            nav.speed = data.walkSpeed;
            state = enemyState.Walk;
        }
    }

    protected override void Walk()
    {
        Debug.Log("Walk");
        if (GetDistance() > data.activeDistance)
        {
            nav.SetDestination(transform.position);
            state = enemyState.Idle;
        }

        if (GetDistance() <= data.attackDistance)
        {
            OnAttack();
        }
    }

    protected override void Run()
    {
        Debug.Log("run");
        if (GetDistance() <= data.attackDistance)
        {
            OnAttack();
        }
    }

    protected override void Attack()
    {
        if (!isAttack)
        {
            StartCoroutine(DoAttack());
        }
    }

    protected override void Stun()
    {
        if (!isStun)
        {
            StartCoroutine(OnStun());
        }
    }

    protected override void OnDie()
    {
        Debug.Log("die");
        Destroy(gameObject);
    }

    public override void OnDamaged(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            state = enemyState.Die;
        }
        else
        {
            state = enemyState.Stun;
        }
    }

    private IEnumerator DoAttack()
    {
        isAttack = true;
        Debug.Log("Attack");

        Collider[] cols = Physics.OverlapSphere(transform.position + transform.forward, data.attackRadius, 1 << 6);

        if (cols != null)
        {
            foreach (Collider col in cols)
            {
                col.GetComponent<IDamage>().OnDamaged(data.attackPower);
            }
        }

        yield return new WaitForSeconds(data.attackDelay);

        isAttack = false;

        if (GetDistance() > data.attackDistance)
        {
            nav.SetDestination(playerTrm.position);
            state = lastState;
        }
    }

    private IEnumerator OnStun()
    {
        isStun = true;
        Debug.Log("Stun");

        if (isAttack) { StopAttack(); }
        nav.SetDestination(transform.position);

        yield return new WaitForSeconds(data.stunTime);

        nav.SetDestination(playerTrm.position);
        nav.speed = data.runSpeed;

        state = enemyState.Run;

        isStun = false;
    }
    private void OnAttack()
    {
        nav.SetDestination(transform.position);
        lastState = state;
        state = enemyState.Attack;
    }

    private void StopAttack()
    {
        StopCoroutine(DoAttack());
        isAttack = false;
    }
}
