using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    float a;
    private void Update(){
        transform.rotation = Quaternion.Euler(0,a,0);
        a+=Time.deltaTime;

        if(a == 360){
            a=0;
        }
    }
}
