using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    private Transform playerTrm;
    private EnemyBase enemy;
    private Transform hpBar;
    private float originXPos;
    private float originXScale;

    private void Start()
    {
        playerTrm = GameObject.Find("Player").GetComponent<Transform>();
        enemy = transform.parent.GetComponent<EnemyBase>();
        hpBar = transform.Find("Hp").GetComponent<Transform>();

        originXScale = hpBar.transform.localScale.x;
        originXPos = hpBar.transform.localPosition.x;
    }

    private void Update()
    {
        Rotate();
        Scale();
    }

    private void Rotate()
    {
        Vector3 dir = (playerTrm.position - transform.position).normalized;

        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(transform.localRotation.x, angle, transform.localRotation.z);
    }

    private void Scale()
    {
        float value = enemy.Health / enemy.MaxHealth * originXScale;

        hpBar.localScale = new Vector3(value, hpBar.localScale.y, hpBar.localScale.z);
        hpBar.localPosition = new Vector3((originXScale - value) * 0.5f, hpBar.localPosition.y, hpBar.localPosition.z);
    }
}
