using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : EnemyBase
{
    public Transform hitPos;
    public float attackRange = 0f;

    public override void Attacking(Transform target)
    {
        if (Distance <= minAttackDistance)
        {
            animator.SetTrigger("Attack");
            PlayAttackSound1();
        }
        else
        {
            EndAttack();
        }

        Debug.Log(Distance);
    }

    public void CheckHit()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position + transform.forward * 1.7f, attackRange, playerLayer);

        if (hit.Length > 0)
        {
            hit[0].GetComponent<IDamage>().OnDamaged(damage);
            Debug.Log("attack123123");
        }
        Debug.Log("asdasd");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1.7f, attackRange);
    }
}
