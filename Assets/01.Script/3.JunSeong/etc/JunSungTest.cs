using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunSungTest : MonoBehaviour, IDamage
{
    CharacterController characterController;

    public void OnDamaged(int damage)
    {
        Debug.Log(damage);
    }

    /*void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(Vector3.zero);  
    }*/
}
