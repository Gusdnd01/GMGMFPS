using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : Weapon
{
    [SerializeField] private Transform firePos;
    private int curBullet;
    protected InputState state;
    [SerializeField] protected TextMeshProUGUI bulletTxt;
    [SerializeField] protected LayerMask layer;

    protected virtual void Awake()
    {
        curBullet = data.bulletAmount;
        bulletTxt = GameObject.Find("Canvas/BulletUI/CurBullet").GetComponent<TextMeshProUGUI>();
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

        Debug.DrawRay(firePos.transform.position, firePos.transform.forward * 150, Color.yellow, 0.5f);
        if (Physics.Raycast(firePos.transform.position, firePos.transform.forward, out hit, layer))
        {
            //hit.transform.gameObject.GetComponent<IHitAble>()?.Hit(data.dmg, firePos.position);
            print(hit.transform.gameObject.name);
        }
    }
}