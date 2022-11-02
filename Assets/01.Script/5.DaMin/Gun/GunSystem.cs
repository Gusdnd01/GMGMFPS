using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunSystem : MonoBehaviour
{

    [Header("Gun Setting")]
    [SerializeField] private GunSetting gunSet;
    [SerializeField] private Recoil recoil;

    [Header("GunSoundSetting")]
    [SerializeField] private AudioSource mysfx;
    //[SerializeField] private GunSoundSetting gunSound;
    private int gunSoundCount;

    private int curbullet;
    private int bulletsShot;

    //bools 
    private bool shooting;
    private bool readyToShoot;
    private bool reloading;


    [Header("Reference")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private RaycastHit rayHit;
    [SerializeField] private LayerMask Tag;

    [Header("Graphics")]
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject bulletHoleGraphic;


    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        curbullet = gunSet.MagazineSize;
        readyToShoot = true;
        recoil = GameObject.Find("Main Camera").GetComponent<Recoil>();
    }
    private void Update()
    {
        MyInput();

        //SetText
        text.SetText(curbullet + " / " + gunSet.MagazineSize);
    }
    private void MyInput()
    {
        if (gunSet.AllowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && curbullet < gunSet.MagazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && curbullet > 0)
        {
            bulletsShot = gunSet.BulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {

        GunShotSound();

        recoil.RecoilFire();
        //GunCameraShake.Instance.ShakeCamera(gunSet.Intensity, gunSet.Shaketime);

        readyToShoot = false;

        //Spread
        float x = Random.Range(-gunSet.Spread, gunSet.Spread);
        float y = Random.Range(-gunSet.Spread, gunSet.Spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, gunSet.Range, Tag))
        {
            Debug.Log(rayHit.collider.name);
            Debug.DrawRay(fpsCam.transform.position, direction * gunSet.Range, Color.red);

            // if (rayHit.collider.CompareTag("Enemy"))
            // {
            //     //rayHit.collider.GetComponent<enemy>().TakeDamage(damage);
            // }

            // if (rayHit.collider.CompareTag("Player"))
            // {
            //     Debug.Log("굿");
            // }
        }


        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.Euler(0, 270, 0));

        curbullet--;
        bulletsShot--;

        Invoke("ResetShot", gunSet.TimeBetweenShooting);

        if (bulletsShot > 0 && curbullet > 0)
            Invoke("Shoot", gunSet.TimeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        GunReloadSound();
        reloading = true;
        Invoke("ReloadFinished", gunSet.ReloadTime);
    }
    private void ReloadFinished()
    {
        curbullet = gunSet.MagazineSize;
        reloading = false;
    }

    private void GunShotSound()
    {
        if (gunSoundCount < gunSet.reloadSound.Count)
        {
            gunSoundCount = 0;
        }
        mysfx.PlayOneShot(gunSet.shotSound[gunSoundCount]);
        gunSoundCount++;
    }

    private void GunReloadSound()
    {
        int ran = Random.Range(0, gunSet.reloadSound.Count);
        mysfx.PlayOneShot(gunSet.reloadSound[ran]);
    }
}
