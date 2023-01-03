using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : EnemyBase
{
    private enum AttackPos
    {
        center,
        front
    }
    private AttackPos attackPos;

    [Header("앞발 휘두르기")]
    public int wieldDamage = 20;
    public float wieldRange = 2f;
    public float wieldAttackDis = 4.75f;
    public float wieldTime;

    private int wieldHash = Animator.StringToHash("Wield");

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        GetComponent<EnemyFSM>().SetAttackPattern(this);
    }

    public override void Attacking(Transform target)
    {
        if (Distance <= wieldAttackDis)
        {
            Wield();
            PlayAttackSound1();
        }
        else
        {
            EndAttack();
        }
    }

    private void Wield()
    {
        Debug.Log("Wield");

        animator.SetTrigger(wieldHash);

        attackPos = AttackPos.front;
        damage = wieldDamage;
    }

    public void CheckHit()
    {
        Vector3 attackingPos = Vector3.zero;

        if (attackPos == AttackPos.center)
        {
            attackingPos = transform.position;
        }
        else
        {
            attackingPos = transform.position + transform.forward * 2;
        }

        Collider[] hit = Physics.OverlapSphere(attackingPos, wieldRange, playerLayer);

        if (hit.Length > 0)
        {
            hit[0].GetComponent<IDamage>().OnDamaged(damage);
            Debug.Log("attack123123");
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        /*Gizmos.DrawWireSphere(transform.position, 3.5f);
        Gizmos.DrawWireSphere(transform.position, 7);
        Gizmos.DrawWireSphere(transform.position, 10);*/
    }
#endif
}
