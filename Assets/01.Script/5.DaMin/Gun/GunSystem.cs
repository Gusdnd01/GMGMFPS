using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.UI;

public class GunSystem : MonoBehaviour
{

    [Header("Gun Setting")]
    [SerializeField] private GunSetting gunSet;
    [SerializeField] private float Power = 40;

    private Recoil recoil;

    private Recoil camRecoil;

    [Header("GunSoundSetting")]
    [SerializeField] private AudioSource mysfx;
    private int gunSoundCount;
    private LineRenderer lineRenderer;

    public int curbullet;
    private int bulletsShot;
    private int mana;
    private float reloadTime;
    private float reloadingTime;

    //bools 
    private bool shooting;
    private bool readyToShoot;
    private bool reloading;
    [Space]
    [SerializeField] private ParticleSystem magicBallStart;



    [Header("Reference")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private RaycastHit rayHit;
    [SerializeField] private LayerMask Tag;
    [SerializeField] protected MMF_Player player;


    [Header("Graphics")]
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject bulletHoleGraphic;
    [SerializeField] private RectTransform crosshair;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject BulletOBJ;
    [SerializeField] private Slider ManaGege;

    private bool Shooting = false;
    private float currenSize = 50;
    private GameObject MainCamera;
    private Camera CameraComp;
    private GunCameraShake shake;
    private void Awake()
    {
        curbullet = gunSet.MagazineSize;
        readyToShoot = true;
        MainCamera = GameObject.Find("Main Camera");
        camRecoil = GetComponent<Recoil>();
        CameraComp = MainCamera.GetComponent<Camera>();
        lineRenderer = GetComponent<LineRenderer>();
        shake = MainCamera.GetComponent<GunCameraShake>();
    }
    private void Update()
    {
        MyInput();
        Cross();

        text.SetText(curbullet + " / " + gunSet.MagazineSize);

        ManaGege.value = mana;
    }
    private void MyInput()
    {
        if (gunSet.AllowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting && !reloading && curbullet > 4)
        {
            shooting = true;
            bulletsShot = gunSet.BulletsPerTap;
            LeftShoot();
        }
        else
        {
            shooting = false;
        }


        if (!shooting && curbullet < 100)
        {
            reloadTime += Time.deltaTime;
            if (reloadTime >= 3 && curbullet < 100)
            {
                reloadingTime += 1 * Time.deltaTime;
                curbullet += Mathf.RoundToInt(reloadingTime);
            }
        }
        else
        {
            reloadTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && mana >= 50)
        {
            RightShoot();
            mana = 0;
        }
    }

    private void LeftShoot()
    {
        player.PlayFeedbacks();

        GunShotSound();


        readyToShoot = false;


        float x = Random.Range(-gunSet.Spread, gunSet.Spread);
        float y = Random.Range(-gunSet.Spread, gunSet.Spread);

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;


        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
        GameObject bullet = Instantiate(BulletOBJ, attackPoint.position, Quaternion.identity);
        bullet.transform.forward = directionWithSpread.normalized;
        bullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * Power, ForceMode.Impulse);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        Instantiate(muzzleFlash, attackPoint.position, Quaternion.Euler(0, 270, 0));

        curbullet -= 5;
        bulletsShot--;
        mana += 5;

        Invoke("ResetShot", gunSet.TimeBetweenShooting);

        if (bulletsShot > 0 && curbullet > 0)
            Invoke("Shoot", gunSet.TimeBetweenShots);
    }

    private void RightShoot()
    {
        shake.start = true;
        player.PlayFeedbacks();
        Vector3 direction = fpsCam.transform.forward;

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, gunSet.Range, Tag))
        {
            Debug.Log(rayHit.collider.name);
            Debug.DrawRay(fpsCam.transform.position, direction * gunSet.Range, Color.red);


            StopCoroutine("lineStop");
            lineRenderer.enabled = false;

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, attackPoint.transform.position);
            lineRenderer.SetPosition(1, rayHit.point);

            StartCoroutine("lineStop");
            if (rayHit.collider != null)
            {
                if (rayHit.collider.transform.GetComponent<IDamage>() != null)
                {
                    rayHit.collider.transform.GetComponent<IDamage>().OnDamaged(gunSet.Damage * 3);
                }
            }
        }
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

    private IEnumerator lineStop()
    {
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }

    private void Cross()
    {
        if (shooting || (Input.GetKeyDown(KeyCode.Mouse1) && curbullet >= 30))
        {
            currenSize = Mathf.Lerp(currenSize, gunSet.AimSize, Time.deltaTime * 10);
        }
        else
        {
            currenSize = Mathf.Lerp(currenSize, gunSet.IdleSize, Time.deltaTime * 10);
        }
        crosshair.sizeDelta = new Vector2(currenSize, currenSize);
    }
}
