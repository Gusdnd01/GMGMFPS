using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MoreMountains.Feedbacks;

public class GunSystem : MonoBehaviour
{

    [Header("Gun Setting")]
    [SerializeField] private GunSetting gunSet;

    private Recoil recoil;

    private Recoil camRecoil;

    [Header("GunSoundSetting")]
    [SerializeField] private AudioSource mysfx;
    //[SerializeField] private GunSoundSetting gunSound;
    private int gunSoundCount;
    private LineRenderer lineRenderer;

    private int curbullet;
    private int bulletsShot;

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
    [SerializeField] private Transform GunPos;
    [SerializeField] private Transform GunZoomPos;

    [Header("Graphics")]
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject bulletHoleGraphic;
    [SerializeField] private RectTransform crosshair;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject BulletOBJ;

    private bool Shooting = false;
    private float currenSize = 50;
    private GameObject MainCamera;
    private Camera CameraComp;
    public Vector3 BulletMovePos;

    private void Awake()
    {
        curbullet = gunSet.MagazineSize;
        readyToShoot = true;
        MainCamera = GameObject.Find("Main Camera");
        camRecoil = GetComponent<Recoil>();
        CameraComp = MainCamera.GetComponent<Camera>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        MyInput();
        Cross();

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
            shooting = true;
            bulletsShot = gunSet.BulletsPerTap;
            Shoot();
        }
        else
        {
            shooting = false;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            CameraComp.fieldOfView = Mathf.Lerp(MainCamera.GetComponent<Camera>().fieldOfView, gunSet.Zoom, Time.deltaTime * gunSet.Smooth);
            transform.position = Vector3.MoveTowards(transform.position, GunZoomPos.position, Time.deltaTime * 10);
        }
        else
        {
            CameraComp.fieldOfView = Mathf.Lerp(MainCamera.GetComponent<Camera>().fieldOfView, 60, Time.deltaTime * gunSet.Smooth);
            transform.position = Vector3.MoveTowards(transform.position, GunPos.position, Time.deltaTime * 10);
        }

    }
    private void Shoot()
    {
        player.PlayFeedbacks();

        GunShotSound();

        //recoil.RecoilFire();
        //camRecoil.RecoilFire();
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
            //lineRenderer(attackPoint,direction * gunSet.Range, Mathf.Infinity);

            StopCoroutine("lineStop");
            lineRenderer.enabled = false;

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, attackPoint.transform.position);
            lineRenderer.SetPosition(1, rayHit.point);
            BulletMovePos = new Vector3(rayHit.point.x, rayHit.point.y, rayHit.point.z);
            GameObject bullet = Instantiate(BulletOBJ, attackPoint.position, Quaternion.identity);
            magicBallStart.Play();

            //bullet.transform.position = Vector3.MoveTowards(transform.position, rayHit.point, 5 * Time.deltaTime);
            StartCoroutine("lineStop");
            if (rayHit.collider != null)
            {
                if (rayHit.collider.transform.GetComponent<IDamage>() != null)
                {
                    rayHit.collider.transform.GetComponent<IDamage>().OnDamaged(10);
                }
            }
            // if (rayHit.collider.CompareTag("Enemy"))
            // {
            //     rayHit.collider.GetComponent<enemy>().TakeDamage(damage);
            // }

            // if (rayHit.collider.CompareTag("Player"))
            // {
            //     Debug.Log("굿");
            // }
        }



        //Graphics
        // Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
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

    private IEnumerator lineStop()
    {
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }

    private void Cross()
    {
        if (shooting)
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
