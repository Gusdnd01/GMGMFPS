using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private GunSystem gun;
    private void Start()
    {
        Destroy(this.gameObject, 5);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.GetComponent<IDamage>() != null)
        {
            collision.transform.GetComponent<IDamage>().OnDamaged(10);
            gun = FindObjectOfType<GunSystem>();
            gun.curbullet += 5;
        }
        Destroy(this.gameObject);
    }
}
