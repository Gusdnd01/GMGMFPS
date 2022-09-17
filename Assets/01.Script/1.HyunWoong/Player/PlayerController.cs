using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    [SerializeField]
    private CharacterController player;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private Animator anim;
    readonly int SpeedHash = Animator.StringToHash("Speed");
    float animSpeed = 0;
    float animSpeedGoal = 0;

    private float m_Hp;
    private float m_Speed;
    private bool isJumped;

    private void Start()
    {
        m_Hp = playerData.hp;
        m_Speed = playerData.speed;

        StartCoroutine(Jump());
    }

    private void Update()
    {
        Move();

        Animate();
    }

    private void Animate()
    {
        animSpeed = Mathf.Lerp(animSpeed, animSpeedGoal, Time.deltaTime * 5);
        anim.SetFloat(SpeedHash, animSpeed);
    }

    [ContextMenu("아야해용")]
    private void GetDamage()
    {
        OnDamaged(10);
        print(m_Hp);
    }

    Vector3 moveDir;

    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (player.isGrounded && !isJumped)
        {
            moveDir.y = 0;
        }
        else
        {
            moveDir.y -= Time.deltaTime * 9.8f;
        }

        moveDir = new Vector3(h * m_Speed, moveDir.y ,v * m_Speed);

        moveDir = transform.TransformDirection(moveDir);

        if(h != 0 || v != 0)
        {
            animSpeedGoal = 1;
        }
        else
        {
            animSpeedGoal = 0;
        }

        player.Move(moveDir * Time.deltaTime);
    }

    /// <summary>
    /// 플레이어 도약
    /// </summary>
    private IEnumerator Jump()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && !isJumped);
            isJumped = true;
            moveDir.y = 5f;
            yield return new WaitForSeconds(1f);
            isJumped=false;
        }
    }

    public void OnDamaged(float damage)
    {
        m_Hp -= damage;

        if(m_Hp <= 0f)
        {
            print("Player Die");
            Die();
        }
    }

    private void Die()
    {
        //죽었을 때 실행
    }
}
