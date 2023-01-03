using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private Transform playerTrm;

    private void Start()
    {
        playerTrm = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        MoveScene();
    }

    private void MoveScene()
    {
        if(Vector3.Distance(transform.position, playerTrm.position) <= 2.5f)
        {
            SceneManager.LoadScene(1);
        }
    }
}
