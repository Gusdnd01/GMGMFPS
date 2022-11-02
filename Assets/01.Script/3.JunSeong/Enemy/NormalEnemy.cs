using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemy : EnemyBase
{
    private NavMeshAgent nav;
    private CharacterController characterController;
    private EnemyFSM enemy;
    private Animator animator;

    private Vector3 moveVec;
    public float punchRange;
    public float jumpPower;
    public float jumpSpeed;
    private bool endJump = false;
    private bool isJump = false;
    public float gravity = -9.81f;

    private void Awake() 
    {
        nav = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        enemy = GetComponent<EnemyFSM>();
        animator = GetComponent<Animator>();
        GetComponent<EnemyFSM>().SetAttackPattern(this);
    }

    public override void Attacking(Transform target)
    {
        if (Distance <= 5)
        {
            Punch();
        }
        else
        {
            Jump(target);
        }
    }

    /*public override void Attack(Transform target)
    {
        if(enemy.FlagAttack)
        {
            Punch();
        }
        else if(Vector3.Distance(transform.position, target.position) >= 10)
        {
            if(!isAttack)
            {
                Jump(target);
            }
        }
        else if(Vector3.Distance(transform.position, target.position) < 10)
        {
            if(!isAttack)
            {
                animator.SetBool("Move", true);
                nav.SetDestination(target.position);
                moveVec = new Vector3(nav.velocity.x * enemy.speed, nav.velocity.y, nav.velocity.z * enemy.speed);
            }
        }

        if(!characterController.isGrounded)
        {
            moveVec.y += gravity * Time.deltaTime;
        }
        else
        {
            moveVec.y = 0;
        }

        Debug.Log("Move");

        characterController.Move(moveVec * Time.deltaTime);
    }*/

    private void Punch()
    {
        animator.SetTrigger("Punch");
        Debug.Log("punch");
    }

    private void Jump(Transform target)
    {
        animator.SetTrigger("Jump");
        Debug.Log("Jump");

        StartCoroutine(DoJump(target));
    }

    private IEnumerator DoJump(Transform target)
    {
        Vector3 dir = (target.position - transform.position).normalized;
        moveVec = new Vector3(dir.x * jumpSpeed, characterController.velocity.y + jumpPower, dir.z * jumpSpeed);

        while (true)
        {
            yield return null;

            if (isJump)
            {
                moveVec.y += gravity * Time.deltaTime;

                controller.Move(moveVec * Time.deltaTime);
            }

            if (endJump)
            {
                Debug.Log("end jump");
                isJump = false;
                endJump = false;
                break;
            }
        }
    }

    public void StartJump()
    {
        isJump = true;
    }    

    public void EndJump()
    {
        endJump = true;
    }

    public void CheckHIt(int damage)
    {
        Collider[] col = Physics.OverlapSphere(transform.position + transform.forward, 1.5f, playerLayer);

        if (col.Length > 0)
        {
            col[0].GetComponent<IDamage>().OnDamaged(damage);
            Debug.Log("개같이 처맞");
        }
    }
}
