using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    GunSystem gunSystem;
    private Vector3 movePos;
    void Start()
    {
        gunSystem = FindObjectOfType<GunSystem>();
        movePos = gunSystem.BulletMovePos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePos, Time.deltaTime * 5);
    }
}
