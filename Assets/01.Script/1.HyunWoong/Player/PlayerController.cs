using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour, IDamage
{
    [Header("Animation")]
    [SerializeField] private Animator anim;
    readonly int SpeedHash = Animator.StringToHash("Speed");
    float animSpeed = 0;
    float animSpeedGoal = 0;

    [Header("Player")]
    [SerializeField] private CharacterController player;
    [SerializeField] private PlayerData playerData;
    private float m_Hp;
    private float m_Speed;
    public float currentHp; //이설아가 pulic으로 바꿈! 허락받음!!!
    private bool isDie = false;
    private bool isJumped;
    public string _tag;

    [Header("Volume Property")]
    [SerializeField] protected MMF_Player feedbacks;
    [SerializeField] VolumeProfile volumeProfile;
    Vignette vignette;
    private float intencity = 0;
    private bool isAya = false;

    private void Awake()
    {
        volumeProfile.TryGet(out vignette);
        vignette.intensity.Override(0);
    }

    private void Start()
    {
        m_Hp = playerData.hp;
        currentHp = m_Hp;
        m_Speed = playerData.speed;

        StartCoroutine(Jump());
        StartCoroutine(Dash());
    }

    private void Update()
    {
        Move();

        Animate();

        VignetteOverride();
    }

    private void VignetteOverride()
    {
        if (isAya)
        {
            
        }
    }

    private IEnumerator Dash()
    {
        while (!isDie)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));

            vignette.color.Override(Color.white);
            m_Speed *= 4f;
            feedbacks.PlayFeedbacks();
            yield return new WaitForSeconds(0.3f);
            m_Speed /= 4;
            yield return new WaitForSeconds(2f);
        }
    }

    private void Animate()
    {
        animSpeed = Mathf.Lerp(animSpeed, animSpeedGoal, Time.deltaTime * 5);
        anim.SetFloat(SpeedHash, animSpeed);
    }

    [ContextMenu("�ƾ��ؿ�")]
    private void GetDamage()
    {
        OnDamaged(10);
        print(currentHp);
    }

    Vector3 moveDir;

    /// <summary>
    /// �÷��̾� ������
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

        moveDir = new Vector3(h * m_Speed, moveDir.y, v * m_Speed);

        moveDir = transform.TransformDirection(moveDir);

        if (h != 0 || v != 0)
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
    /// �÷��̾� ����
    /// </summary>
    private IEnumerator Jump()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && !isJumped);
            isJumped = true;
            moveDir.y = 5f;
            yield return new WaitForSeconds(1f);
            isJumped = false;
        }
    }

    public void OnDamaged(int damage)
    {
        currentHp -= damage;
        print(currentHp/m_Hp);
        if (currentHp/m_Hp <= 0.3f)// 100 <= 0.3f
        {
            intencity = Mathf.Lerp(intencity, 0.3f, Time.deltaTime);
            vignette.color.Override(Color.red);
            vignette.intensity.Override(intencity);
        }

        if (m_Hp <= 0f)
        {
            print("Player Die");
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            Die();
        }
    }

    private void Die()
    {
        print("떨어져서 죽음, GameOver");

        Time.timeScale = 0;
    }
}
