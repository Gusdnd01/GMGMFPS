using System;
using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    protected GameObject Player;
    protected AudioSource sound;

    protected float originTurnSpeed; //���� ��� ���� �÷��̾��� ���� ����

    [Header("sound & effect")]
    [SerializeField] protected Transform muzzleTrans;
    [SerializeField] protected Transform powerShotPoint;

    protected RaycastHit hit; // �浹 ����

    [Header("bullet")]
    protected int maxBullet; // �ִ� �Ѿ� ����
    protected int curBullet; // �� źâ�� ���� ���� �ִ� �Ѿ� ����

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
        print("�߻�!");
        PlayAudio(data.attackClip);
        // PlayAudio(data.dryAttackClip); �̰� ��� ����ұ�?
        // TODO : �߻�� �۵��� �ڵ� �ۼ� EX : �ݵ�, Raycast ���
        RaycastHit hit;
        yield return new WaitForSeconds(data.attackDelay);
        callbackAction();   // �޾ƿ� �Լ� ����
    }

    public override void Reload(Action callbackAction)
    {
        StartCoroutine(ReloadCor(callbackAction));
    }

    public virtual IEnumerator ReloadCor(Action callbackAction)
    {
        print("����!");
        PlayAudio(data.reloadClip);
        // TODO : ������ �ؾ��� �ൿ EX : �Ѿ� ü��� ���
        yield return new WaitForSeconds(data.reloadDelay);
        callbackAction();   // �޾ƿ� �Լ� ����
    }

    public override void RightClick(Action callbackAction)
    {
        StartCoroutine(RightClickCor(callbackAction));
    }

    public virtual IEnumerator RightClickCor(Action callbackAction)
    {
        print("Ư���ൿ!");
        // TODO : Ư���ൿ ���� EX : ������, ��Ÿ �Ѹ��� ��ų
        yield return new WaitForSeconds(data.zoomDelay);
        callbackAction();   // �޾ƿ� �Լ� ����
    }

    protected virtual void ShootRay()
    {
        // TODO : Raycast ó��
    }
}