using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GetHurtUI : MonoBehaviour
{
    [SerializeField] GameObject image;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Damaged();
        }
    }

    public void Damaged()
    {
        image.GetComponent<Image>().color = Color.Lerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 0.2f), 0.1f);
        Invoke("BeOrigine", 1);
    }

    void BeOrigine()
    {
        image.GetComponent<Image>().color = Color.Lerp(new Color(1, 0, 0, 0.2f), new Color(1, 0, 0, 0), 0.1f);
    }
}
