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

    public void Start()                 // WeaponCycle 돌리기
    {
        WeaponSwap();
        StartCoroutine(WeaponCycle());
    }
    public void Update()                // InputCheck 하기
    {
        CheckInput();
    }

    private void CheckInput()           // Update마다 호출됨
    {
        if (curInputState == InputState.None && isCanReadKey)   // 만약 지금 인풋 상태가 None이면(isCanReadKey 는 안전장치)
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
        }                                                       // 입력받은 키에 따라서 InputState 바꿔주기
    }

    private IEnumerator WeaponCycle()   // Start 에서 한번 호출됨
    {
        Action callbackAction = () => { curInputState = InputState.None; };         // InputState 를 None 으로 만들어주는 한줄 함수
        while (true)
        {
            yield return new WaitUntil(() => curInputState != InputState.None);
            isCanReadKey = false;
            switch (curInputState)                          // InputState에 따라 CurWeapon의 함수 실행
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
    //prevWeapon은 끄고 newWeapon은 활성화 하기
    {
        newWeapon.gameObject.SetActive(true);
        prevWeapon.gameObject.SetActive(false);
    }

    enum InputState     // InputState 들
    {
        Attack,
        Reload,
        RightClick,
        None,
        Length
    }
}
