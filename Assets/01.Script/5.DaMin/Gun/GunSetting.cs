using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunSetting : ScriptableObject
{
    [Header("Gun stats")]
    [SerializeField] private int damage;        //데미지
    public int Damage => damage;
    [SerializeField] private float timeBetweenShooting;     //연사 속도
    public float TimeBetweenShooting => timeBetweenShooting;
    [SerializeField] private float spread;      //탄퍼짐
    public float Spread => spread;
    [SerializeField] private float range;       //사정거리
    public float Range => range;
    [SerializeField] private float reloadTime;      //장전시간
    public float ReloadTime => reloadTime;
    [SerializeField] private float timeBetweenShots;        //점사총 연사 간격 (0 초과시 && !allowButtonHold) 
    public float TimeBetweenShots => timeBetweenShots;
    [SerializeField] private int magazineSize;      //탄창크기
    public int MagazineSize => magazineSize;
    [SerializeField] private int bulletsPerTap;     //한번에 발사되는 총알 수
    public int BulletsPerTap => bulletsPerTap;
    [SerializeField] private bool allowButtonHold;      //True = 연사  False = 단발     if(timeBetweenShots > 0 && !allowButtonHold)점사
    public bool AllowButtonHold => allowButtonHold;

    [Header("ShakeCamera")]
    [SerializeField] private float intensity; //흔들림 강도
    public float Intensity => intensity;
    [SerializeField] private float shaketime;
    public float Shaketime => shaketime;
}

