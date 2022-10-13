using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/WeaponData")]
public class WeaponDataSo : ScriptableObject
{
    [Header("�⺻���")]
    public string weaponName = "���� �̸�";
    public string weaponDesc = "���� ����";
    public Vector3 zoomPos;
    [Range(0.05f, 2f)] public float attackDelay = 0.1f;
    [Range(0.5f, 5f)] public float reloadDelay = 2.5f;
    [Range(0.1f, 1f)] public float zoomDelay = 0.2f;
    [Range(1, 100)] public int bulletAmount = 30;
    public int magBullet = 90;

    [Header("�������")]
    public AttackType attackType = AttackType.Auto;
    public Vector2 recoil = Vector2.one;
    [Range(10, 100)] public float dmg = 25;

    [Header("����")]
    public AudioClip attackClip;
    public AudioClip dryAttackClip;
    public AudioClip reloadClip;

    [Header("����")]
    public bool isCanShotGun = false;
    public int multiShootCount = 5;
    public int multiActually = 10;

    [Header("�ݵ�")]
    public Vector3 upRecoil;
    public Vector3 originPos;
}
public enum AttackType
{
    Auto,
    SemiAuto
}