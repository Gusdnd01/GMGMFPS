using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    public enum State
    {
        Ready, // �غ� �Ϸ�
        Shoot, // �߻� ��
        Reloading, // ������
        FineSight, // ������
    }
    public State state { get; protected set; }

    [Header("data")]
    [SerializeField] protected WeaponDataSo data;


    protected GameObject Player;
    protected AudioSource sound;

    protected float originTurnSpeed; //���� ��� ���� �÷��̾��� ���� ����

    [Header("sound & effect")]
    [SerializeField] protected Transform muzzleTrans;
    [SerializeField] protected Transform powerShotPoint;
    //[SerializeField] protected LineRenderer bulletLine;
    //[SerializeField] protected GameObject aimPoint;

    protected RaycastHit hit; // �浹 ����

    [Header("bullet")]
    protected int maxBullet; // �ִ� �Ѿ� ����
    protected int curBullet; // �� źâ�� ���� ���� �ִ� �Ѿ� ����
    //[SerializeField] protected int mag; // �� źâ�� �� �� �ִ� �Ѿ� ����
    //[SerializeField] protected float reloadDelay;

    //[Header("camera")]
    protected Camera cam;

    //[Header("Fire Pos")]
    //protected float curAttackDelay;

    protected void Awake()
    {
        maxBullet = data.bulletCapacity * 5;

        cam = Camera.main;

        state = State.Ready;
        //bulletLine.enabled = false;

        curBullet = data.bulletCapacity;
    }

    protected void Start()
    {
        if (state == State.Ready)
        {
            StartCoroutine(TryFire());
        }
    }

    protected virtual IEnumerator TryFire()
    {
        while (true)
        {
            if (state == State.Ready)
            {
                if (curBullet > 0)
                {
                    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                    state = State.Shoot;
                    LeftClick();

                    yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                    state = State.Ready;
                }
                else
                    StartCoroutine(Reloading());

            }
            yield return new WaitForSeconds(data.attackDelay);
        }
    }

    public override void LeftClick()
    {
        //sound.PlayOneShot(data.shotClip);
        //curAttackDelay = data.attackDelay;
        curBullet--;

        Debug.DrawRay(muzzleTrans.position, muzzleTrans.forward, Color.blue, 0.5f);
        //bulletLine.startWidth = 0.1f;
        //bulletLine.endWidth = 0.01f;
        //bulletLine.SetPosition(0, weaponMuzzle.position);
        //bulletLine.SetPosition(1, aimPoint.transform.position);
        //bulletLine.enabled = true;
        // �߻� ����Ʈ
    }
    protected virtual IEnumerator Reloading()
    {
        state = State.Reloading;
        yield return new WaitForSeconds(data.reloadDelay);
        maxBullet -= data.bulletCapacity - curBullet;
        curBullet = data.bulletCapacity;
        state = State.Ready;
        sound.PlayOneShot(data.reloadClip);
    }

    public override void RightClick()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, data.range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                //IDamageAble
            }
        }
    }

    public override void PressR()
    {
        if (Input.GetKeyDown(KeyCode.R) && state == State.Ready && curBullet < data.bulletCapacity)
        {
            StartCoroutine(Reloading());
        }
    }
}