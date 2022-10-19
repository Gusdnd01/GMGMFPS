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
    [Range(0.05f, 1f)] public float attackDelay = 0.1f;
    [Range(0.5f, 5f)] public float reloadDelay = 2.5f;
    [Range(0.1f, 1f)] public float zoomDelay = 0.2f;
    [Range(1, 100)] public int bulletAmount = 30;
    public int magBullet = 90;

    [Header("�������")]
    public AttackType attackType = AttackType.Auto;
    public Vector2 recoil = Vector2.one;
    [Range(10, 100)] public float dmg = 25;

    [Header("���ǰ���")]
    public bool isMultiShoot;
    public int multiShootCount = 1;
    public float multiActually;//10M ������ �i���� Actually���� ������ ź���� ����

    [Header("����")]
    public AudioClip attackClip;
    public AudioClip dryAttackClip;
    public AudioClip reloadClip;
}
public enum AttackType
{
    Auto,
    SemiAuto
}