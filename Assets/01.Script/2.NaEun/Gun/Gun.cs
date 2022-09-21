using System;
using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    protected GameObject Player;
    protected AudioSource sound;

    protected float originTurnSpeed; //감도 제어를 위한 플레이어의 원래 감도

    [Header("sound & effect")]
    [SerializeField] protected Transform muzzleTrans;
    [SerializeField] protected Transform powerShotPoint;

    protected RaycastHit hit; // 충돌 정보

    [Header("bullet")]
    protected int maxBullet; // 최대 총알 개수
    protected int curBullet; // 한 탄창에 현재 남아 있는 총알 개수

    [Header("camera")]
    protected Camera cam;

    protected void Awake()
    {
        maxBullet = data.bulletAmount * 5;

        cam = Camera.main;
        curBullet = data.bulletAmount;
    }

    protected void Start()
    {

    }

    protected virtual IEnumerator Reloading()
    {
        yield return new WaitForSeconds(data.reloadDelay);
        maxBullet -= (data.bulletAmount - curBullet);
        curBullet = data.bulletAmount;
        sound.PlayOneShot(data.reloadClip);
    }
    [SerializeField] private Transform firePos;

    public override void Attack(Action callbackAction)
    {
        StartCoroutine(AttackCor(callbackAction));
    }

    public virtual IEnumerator AttackCor(Action callbackAction)
    {
        print("발사!");
        PlayAudio(data.attackClip);
        // PlayAudio(data.dryAttackClip); 이걸 어떻게 써야할까?
        // TODO : 발사시 작동될 코드 작성 EX : 반동, Raycast 등등
        RaycastHit hit;
        yield return new WaitForSeconds(data.attackDelay);
        callbackAction();   // 받아온 함수 실행
    }

    public override void Reload(Action callbackAction)
    {
        StartCoroutine(ReloadCor(callbackAction));
    }

    public virtual IEnumerator ReloadCor(Action callbackAction)
    {
        print("장전!");
        PlayAudio(data.reloadClip);
        // TODO : 장전시 해야할 행동 EX : 총알 체우기 등등
        yield return new WaitForSeconds(data.reloadDelay);
        callbackAction();   // 받아온 함수 실행
    }

    public override void RightClick(Action callbackAction)
    {
        StartCoroutine(RightClickCor(callbackAction));
    }

    public virtual IEnumerator RightClickCor(Action callbackAction)
    {
        print("특수행동!");
        // TODO : 특수행동 구현 EX : 정조준, 기타 총마다 스킬
        yield return new WaitForSeconds(data.zoomDelay);
        callbackAction();   // 받아온 함수 실행
    }

    protected virtual void ShootRay()
    {
        // TODO : Raycast 처리
    }
}