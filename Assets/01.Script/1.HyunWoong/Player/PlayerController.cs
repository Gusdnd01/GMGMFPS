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
    private float m_Speed;
    public float currentHp;
    private bool isDie = false;
    private bool isJumped;
    private bool isDashed;
    public string _tag;

    [Header("Volume Property")]
    [SerializeField] protected MMF_Player feedbacks;
    [SerializeField] VolumeProfile volumeProfile;
    Vignette vignette;
    private float intencity = 0;
    private bool isAya = false;

    private PlayerHp playerHp;

    [Header("Sound")]   
    public AudioClip dashSound;
    public AudioClip jumpSound;
    public AudioClip dieSound;
    public AudioClip hitSound;
    public List<AudioClip> footStepSounds = new List<AudioClip>();
    private int footStepSoundIndex = 0;
    private SoundPlay Saudio;

    private void Awake()
    {
        volumeProfile.TryGet(out vignette);
        playerHp = GetComponent<PlayerHp>();
        vignette.intensity.Override(0);
        vignette.color.Override(Color.red);
        audio = GetComponent<SoundPlay>();
        Saudio = GetComponent<SoundPlay>();
    }

    private void Start()
    {
        currentHp = playerData.hp;
        m_Speed = playerData.speed;

        StartCoroutine(Jump());
        StartCoroutine(Dash());
    }

    private void Update()
    {
        Move();

        Animate();

    }


    private IEnumerator Dash()
    {
        while (!isDie)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));

            Saudio.PlaySound(dashSound);
            vignette.color.Override(Color.white);
            m_Speed *= 4f;
            feedbacks.PlayFeedbacks();
            isDashed = true;
            yield return new WaitForSeconds(0.3f);
            m_Speed /= 4;
            isDashed = false;
            yield return new WaitForSeconds(2f);
        }
    }

    private void Animate()
    {
        animSpeed = Mathf.Lerp(animSpeed, animSpeedGoal, Time.deltaTime * 5);
        anim.SetFloat(SpeedHash, animSpeed);
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

            if(!isJumped && !isDashed && !Saudio.IsPlaying())
            {
                Saudio.PlaySound(footStepSounds[footStepSoundIndex]);
                footStepSoundIndex++;

                if(footStepSoundIndex >= footStepSounds.Count)
                {
                    footStepSoundIndex = 0;
                }
            }
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
            Saudio.PlaySound(jumpSound);
            isJumped = true;
            moveDir.y = 5f;
            yield return new WaitForSeconds(1f);
            isJumped = false;
        }
    }

    public void OnDamaged(int damage)
    {
        print("Damaged");
        currentHp -= (float)damage;
        playerHp.ModifyHp(currentHp);
        print($"currentHp: {currentHp}");
        vignette.intensity.Override((playerData.hp - currentHp)/200);
        if (currentHp <= 0f)
        {
            print("Player Die");
            Die();
        }
        else
        {
            Saudio.PlaySound(hitSound);
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
        Saudio.PlaySound(dieSound);
        Time.timeScale = 0;
    }
}
