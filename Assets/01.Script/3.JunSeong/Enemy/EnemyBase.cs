using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EnemyBase : MonoBehaviour, IDamage
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float turnSpeed;
    [SerializeField] private float gravityScale = -9.81f;

    public float minAttackDistance;
    public bool endAttack;

    public float MoveSpeed { get => moveSpeed; }
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    protected CharacterController controller;
    protected Transform player;
    protected Animator animator;

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

        health = maxHealth;
    }

    public abstract void Attacking(Transform target);

    private void Update()
    {
        if (!CheckGround())
        {
            controller.Move(new Vector3(0 ,gravityScale,0) * Time.deltaTime);
        }
    }

    public void Move()
    {
        Vector3 moveVector = new Vector3(MoveDirection.x * moveSpeed, 0, MoveDirection.z * moveSpeed);
        
        controller.Move(moveVector * Time.deltaTime);
        Debug.Log("move");

        Turn();
    }

    public void Turn()
    {
        float angle = Mathf.Atan2(MoveDirection.x, MoveDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * turnSpeed);
    }
    public void EndAttack()
    {
        Debug.Log("endAttack");
        endAttack = true;
    }

    public void Die()
    {
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>().Remove(this.gameObject);

        Debug.Log("die");
        animator.SetTrigger("Die");
        //보스 아니면 오브젝트풀로 넣어주기
    }
    public bool CheckAngle()
    {
        Vector2 targetDir = player.position - transform.position.normalized;
        float angle = 10f;

        return Mathf.Abs(Vector3.Dot(transform.forward, targetDir)) < angle;
    }
    private bool CheckGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.2f, 1 << 9);
    }

    public void OnDamaged(int damage)
    {
        health -= damage;
        Debug.Log("damaged");
    }
}
