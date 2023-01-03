using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private Transform playerTrm;
    private JSON json;

    private void Start()
    {
        playerTrm = GameObject.Find("Player").transform;
        json = FindObjectOfType<JSON>();
    }

    private void Update()
    {
        MoveScene();
    }

    private void MoveScene()
    {
        if (Vector3.Distance(transform.position, playerTrm.position) <= 2.5f)
        {
            int NowScene = SceneManager.GetActiveScene().buildIndex;
            switch (NowScene)
            {
                case (2):
                    json.saveData.stage1 = true;
                    break;
                case (3):
                    json.saveData.stage3 = true;
                    break;
                case (4):
                    json.saveData.stage2 = true;
                    break;
                default:
                    break;
            }
            json.Save();
            SceneManager.LoadScene(1);
        }
    }
}
