using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndLineer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<GameEnd>().GameClear();
        }
    }
}
