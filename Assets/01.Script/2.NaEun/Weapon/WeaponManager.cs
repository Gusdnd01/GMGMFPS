using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<Weapon> weapons = new List<Weapon>();
    int curWeaponIndex = 0;
    [SerializeField] private Weapon curWeapon;
    private bool isCanReadKey = true;

    private InputState curInputState = InputState.None;

    public void Start()                 // WeaponCycle ������
    {
        StartCoroutine(WeaponCycle());
    }
    public void Update()                // InputCheck �ϱ�
    {
        CheckInput();
        WeaponSwap();
    }

    private void CheckInput()           // Update���� ȣ���
    {
        if (curInputState == InputState.None && isCanReadKey)   // ���� ���� ��ǲ ���°� None�̸�(isCanReadKey �� ������ġ)
        {
            switch (curWeapon.Data.attackType)
            {
                case AttackType.Auto:
                    if (Input.GetKey(KeyCode.Mouse0)) // �� ���� üũ
                    {
                        curInputState = InputState.Attack;
                    }
                    break;
                case AttackType.SemiAuto:
                    if (Input.GetKeyDown(KeyCode.Mouse0)) // �� ���� üũ
                    {
                        curInputState = InputState.Attack;
                    }
                    break;
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                curInputState = InputState.RightClick;
            }
            if (Input.GetKey(KeyCode.R))
            {
                curInputState = InputState.Reload;
            }
        }                                                       // �Է¹��� Ű�� ���� InputState �ٲ��ֱ�
    }

    private IEnumerator WeaponCycle()   // Start ���� �ѹ� ȣ���
    {
        Action callbackAction = () => { curInputState = InputState.None; };         // InputState �� None ���� ������ִ� ���� �Լ�
        while (true)
        {
            yield return new WaitUntil(() => curInputState != InputState.None);
            isCanReadKey = false;
            switch (curInputState)                          // InputState�� ���� CurWeapon�� �Լ� ����
            {
                case InputState.Attack:
                    curWeapon.Attack(callbackAction);
                    break;
                case InputState.Reload:
                    curWeapon.Reload(callbackAction);
                    break;
                case InputState.RightClick:
                    curWeapon.RightClick(callbackAction);
                    break;
            }
            yield return new WaitUntil(() => curInputState == InputState.None);
            isCanReadKey = true;
        }
    }

    protected virtual void WeaponSwap()
    {
        float mouseWheel = Input.GetAxisRaw("Mouse ScrollWheel");
        if (mouseWheel != 0)
        {
            int newIndex = curWeaponIndex + (mouseWheel > 0 ? 1 : -1);

            newIndex = newIndex % weapons.Count;

            if (newIndex < 0)
            {
                newIndex = weapons.Count - 1;
            }

            print($"Prev : {curWeaponIndex}  New : {newIndex}");
            ChangeWeapon(weapons[newIndex], weapons[curWeaponIndex]);
            curWeaponIndex = newIndex;
            curWeapon = weapons[curWeaponIndex];
        }
    }
    protected void ChangeWeapon(Weapon newWeapon, Weapon prevWeapon)
    {
        newWeapon.StopAllCoroutines();
        prevWeapon.StopAllCoroutines();

        newWeapon.gameObject.SetActive(true);
        prevWeapon.gameObject.SetActive(false);
    }
}
public enum InputState     // InputState ��
{
    Attack,
    Reload,
    RightClick,
    None,
    Length
}