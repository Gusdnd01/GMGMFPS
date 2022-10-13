using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/WeaponData")]
public class WeaponDataSo : ScriptableObject
{
    [Header("기본재원")]
    public string weaponName = "무기 이름";
    public string weaponDesc = "무기 설명";
    public Vector3 zoomPos;
    [Range(0.05f, 2f)] public float attackDelay = 0.1f;
    [Range(0.5f, 5f)] public float reloadDelay = 2.5f;
    [Range(0.1f, 1f)] public float zoomDelay = 0.2f;
    [Range(1, 100)] public int bulletAmount = 30;
    public int magBullet = 90;

    [Header("성능재원")]
    public AttackType attackType = AttackType.Auto;
    public Vector2 recoil = Vector2.one;
    [Range(10, 100)] public float dmg = 25;

    [Header("사운드")]
    public AudioClip attackClip;
    public AudioClip dryAttackClip;
    public AudioClip reloadClip;

    [Header("샷건")]
    public bool isCanShotGun = false;
    public int multiShootCount = 5;
    public int multiActually = 10;

    [Header("반동")]
    public Vector3 upRecoil;
    public Vector3 originPos;
}
public enum AttackType
{
    Auto,
    SemiAuto
}