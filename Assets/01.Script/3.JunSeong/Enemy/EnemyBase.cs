using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected int Health;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float turnSpeed;

    public float minAttackDistance;
    public bool endAttack;

    public float MoveSpeed { get => moveSpeed; }

    protected CharacterController controller;
    protected Transform player;

    protected float Distance
    {
        get
        {
            return Vector3.Distance(transform.position, GameObject.Find("Player").GetComponent<Transform>().position);
        }
    }   
    
    protected Vector3 MoveDirection
    {
        get
        {
            return (GameObject.Find("Player").GetComponent<Transform>().position - transform.position).normalized;
        }
    }    

    protected LayerMask playerLayer = 1 << 7;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
    }

    public abstract void Attacking(Transform target);

    public void Move()
    {
        Vector3 moveVector = new Vector3(MoveDirection.x * moveSpeed, transform.localPosition.y, MoveDirection.z * moveSpeed);
        controller.Move(moveVector * Time.deltaTime);

        Turn();
    }

    public void Turn()
    {
        //È¸Àü
        float angle = Mathf.Atan2(MoveDirection.x, MoveDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * turnSpeed);
    }

    public void EndAttack()
    {
        endAttack = true;
    }

    public bool CheckAngle()
    {
        Vector2 targetDir = player.position - transform.position;
        float angle = 10f;

        return Vector3.Dot(transform.forward, targetDir) > Mathf.Cos((angle * 0.5f) * Mathf.Deg2Rad);
    }
}
