using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChange : MonoBehaviour
{
    [SerializeField] private List<GameObject> GunList = new List<GameObject>();
    private int currenGun = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //GunList[currenGun];
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0)
        {
            // 휠을 밀어 돌렸을 때의 처리 ↑
            //GunList[] ++;
        }
        else if (wheelInput < 0)
        {
            // 휠을 당겨 올렸을 때의 처리 ↓
        }
    }
}
