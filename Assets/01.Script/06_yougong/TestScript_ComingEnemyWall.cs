using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript_ComingEnemyWall : MonoBehaviour
{
    Vector3 direction = Vector3.zero;

    private Transform _player = null;
    public Transform Player
    {
        get
        {
            if(_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return _player;
        }
    }

    void Update()
    {
        direction = (Player.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * 5 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            Player.GetComponent<PlayerController>().OnDamaged(99999);
    }
}
