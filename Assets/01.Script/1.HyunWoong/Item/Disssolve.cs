using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disssolve : MonoBehaviour
{
    Material[] mat;
    bool isDissolve = false;
    float fade = -1f;

    void Awake()
    {
        mat = GetComponent<MeshRenderer>().materials;
    }
    public bool IsDissolve
    {
        get => isDissolve;
        set => isDissolve = value;
    }

    void Update()
    {
        if (isDissolve)
        {
            print("Dissolve");
            Dissolve();
        }
    }

    private void Dissolve()
    {
        if (isDissolve)
        {
            fade += Time.deltaTime * 1.32f;
            if (fade > 2f)
            {
                fade = 2f;
                isDissolve = false;
            }
            foreach(Material m in mat)
            {
                m.SetFloat("_Cutoff_Height", fade);
            }
        }
    }
}
