using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleMousePoint : MonoBehaviour
{
    ESCLoader loader;

    private void Start()
    {
        loader = FindObjectOfType<ESCLoader>();
    }

    private void Update()
    {
        if(!loader.isPausing)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
