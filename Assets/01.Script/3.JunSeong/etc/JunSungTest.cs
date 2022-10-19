using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunSungTest : MonoBehaviour
{
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(Vector3.zero);  
    }
}
