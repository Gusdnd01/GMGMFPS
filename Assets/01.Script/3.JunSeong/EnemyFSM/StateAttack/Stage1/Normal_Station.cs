using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Normal_Station : AttackPattern
{
    private NavMeshAgent nav;
    private CharacterController characterController;
    private EnemyFSM enemy;
    private Animator animator;

    private Vector3 moveVec;
    public bool isAttack = false;
    public bool isJump = false;
    public float jumpPower;
    public float jumpSpeed;
    public float gravity = -9.81f;

    private void Awake() 
    {
        nav = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        enemy = GetComponent<EnemyFSM>();
        animator = GetComponent<Animator>();
    }

    public override void Attack(Transform target)
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
    }

    private void Punch()
    {
        if(!isAttack)
        {
            animator.SetTrigger("Punch");
            isAttack = true;
            moveVec = Vector3.zero;
            Debug.Log("punch");
        }
    }

    private void Jump(Transform target)
    {
        if(!isAttack)
        {
            animator.SetTrigger("Jump");
            Debug.Log("Jump");
            isAttack = true;
            Vector3 dir = (target.position - transform.position).normalized;
            moveVec = new Vector3(dir.x * jumpSpeed, characterController.velocity.y + jumpPower, dir.z * jumpSpeed);
            Debug.LogWarning($"Move Vec : {moveVec}, JumpSpeed : {jumpSpeed})");
        }
    }

    public void EndAttack()//animation event
    {
        isAttack = false;
        moveVec = Vector3.zero;
    }

    public void EndJump()//animation event
    {
        moveVec = Vector3.zero;
    }
}
