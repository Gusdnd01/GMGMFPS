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
        // PlayAudio(data.dryAttackClip); 이걸 어떻게 써야할까?
        // TODO : 발사시 작동될 코드 작성 EX : 반동, Raycast 등등\while (true)
        if (curBullet > 0)
        {
            curBullet--;

            ShootRay();

            AudioManager.PlayAudioRandPitch(data.attackClip);
            print("ㅎ호");
        }
        else
        {
            print("ㄹㄹㅎㅎㄴㄴ");
            AudioManager.PlayAudioRandPitch(data.dryAttackClip);
            // 틱 틱 틱..!
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
        // TODO : 장전시 해야할 행동 EX : 총알 채우기 등등
        data.magBullet -= (data.bulletAmount - curBullet);
        curBullet = data.bulletAmount;
        yield return new WaitForSeconds(data.reloadDelay);
        bulletTxt.text = $"{curBullet} / {data.magBullet}";
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
        RaycastHit hit;

        Debug.DrawRay(firePos.transform.position, firePos.transform.forward * 150, Color.yellow, 0.5f);
        if (Physics.Raycast(firePos.transform.position, firePos.transform.forward, out hit, layer))
        {
            //hit.transform.gameObject.GetComponent<IHitAble>()?.Hit(data.dmg, firePos.position);
            print(hit.transform.gameObject.name);
        }
    }
}