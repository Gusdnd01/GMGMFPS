using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : Weapon
{
    [SerializeField] protected Transform firePos;
    protected int curBullet;
    protected InputState state;
    [SerializeField] protected TextMeshProUGUI bulletTxt;
    [SerializeField] protected LayerMask layer;
    protected PlayerController pc;
    protected int count = 5;

    protected virtual void Awake()
    {
        curBullet = data.bulletAmount;
        bulletTxt = GameObject.Find("Canvas/BulletUI/CurBullet").GetComponent<TextMeshProUGUI>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        bulletTxt.text = $"{curBullet} / {data.magBullet}";
    }

    public override void Attack(Action callbackAction)
    {
        StartCoroutine(AttackCor(callbackAction));
    }

    public virtual IEnumerator AttackCor(Action callbackAction)
    {

        bulletTxt.text = $"{curBullet} / {data.magBullet}";
        // PlayAudio(data.dryAttackClip); �̰� ��� ����ұ�?
        // TODO : �߻�� �۵��� �ڵ� �ۼ� EX : �ݵ�, Raycast ���\while (true)
        if (curBullet > 0)
        {
            curBullet--;
            ShootRay();

            AudioManager.PlayAudioRandPitch(data.attackClip);

            print("��ȣ");
        }
        else
        {
            print("������������");
            AudioManager.PlayAudioRandPitch(data.dryAttackClip);
            // ƽ ƽ ƽ..!
        }

        yield return new WaitForSeconds(data.attackDelay);

        callbackAction();
    }

    public override void Reload(Action callbackAction)
    {
        StartCoroutine(ReloadCor(callbackAction));
    }

    public virtual IEnumerator ReloadCor(Action callbackAction)
    {
        PlayAudio(data.reloadClip);
        // TODO : ������ �ؾ��� �ൿ EX : �Ѿ� ä��� ���
        data.magBullet -= (data.bulletAmount - curBullet);
        curBullet = data.bulletAmount;
        if (data.bulletAmount <= 0)
        {
            callbackAction();
        }
        yield return new WaitForSeconds(data.reloadDelay);
        bulletTxt.text = $"{curBullet} / {data.magBullet}";
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
        RaycastHit hit;
        float reactionRollback = Random.Range(-2.5f, 2.5f);

        Debug.DrawRay(firePos.transform.position, firePos.transform.forward, Color.yellow, 0.5f);
        //StartCoroutine(Recoil());
        if (data.isCanShotGun)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 randPos = firePos.position + firePos.forward * 10 + firePos.TransformDirection(Random.insideUnitCircle);
                Vector3 dir = (randPos - firePos.position).normalized;

                Debug.DrawRay(firePos.transform.position, dir * 10, Color.yellow, 0.5f);
                if (Physics.Raycast(firePos.transform.position, dir, out hit, 10, layer))
                {
                    //hit.transform.gameObject.GetComponent<IHitAble>()?.Hit(data.dmg, firePos.position);
                    //�ѱ� �ݵ� �ڷ�ƾ ����
                    print(hit.transform.gameObject.name);
                }
            }
        }
        else
        {
            if (Physics.Raycast(firePos.transform.position, firePos.transform.forward, out hit, layer))
            {
                //hit.transform.gameObject.GetComponent<IHitAble>()?.Hit(data.dmg, firePos.position);
                //�ѱ� �ݵ� �ڷ�ƾ ����
                print(hit.transform.gameObject.name);
            }
        }
    }

    protected IEnumerator Recoil()
    {
        transform.localEulerAngles += data.upRecoil;
        //tansform.localEulerAngles -= data.upRecoil * Time.deltaTime * data.reloadDelay;
        yield return null;
    }
}