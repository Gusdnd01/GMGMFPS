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
        WeaponSwap();
        StartCoroutine(WeaponCycle());
    }
    public void Update()                // InputCheck �ϱ�
    {
        CheckInput();
    }

    private void CheckInput()           // Update���� ȣ���
    {
        if (curInputState == InputState.None && isCanReadKey)   // ���� ���� ��ǲ ���°� None�̸�(isCanReadKey �� ������ġ)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                curInputState = InputState.Attack;
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
        }
    }

    private void ChangeWeapon(Weapon newWeapon, Weapon prevWeapon)
    //prevWeapon�� ���� newWeapon�� Ȱ��ȭ �ϱ�
    {
        newWeapon.gameObject.SetActive(true);
        prevWeapon.gameObject.SetActive(false);
    }

    enum InputState     // InputState ��
    {
        Attack,
        Reload,
        RightClick,
        None,
        Length
    }
}
