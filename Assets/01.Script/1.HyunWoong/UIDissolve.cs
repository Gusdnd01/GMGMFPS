using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDissolve : MonoBehaviour
{
    private bool _isDissolve = false;
    public bool IsDissolved{
        get => _isDissolve;
        set => _isDissolve = value;
    }

    float fade;

    public Material material{
        get;
        set;
    }

    private void Start()
    {
        //fade = 1000;
        //material.SetFloat("_Cutoff_Height", fade);
    }

    IEnumerator DissolveMethod()
    {
        while (true)
        {
            if (_isDissolve)
            {
                fade += 3;

                if (fade >= 1000)
                {
                    fade = 1000;
                    StopCoroutine(DissolveMethod());
                }
            }

            else
            {
                fade -= 3;

                if (fade <= -1000)
                {
                    fade = -1000;
                    StopCoroutine(DissolveMethod());
                }
            }
            material.SetFloat("_Cutoff_Height", fade);
            yield return null;
        }
    }

    public void IsDissolve()
    {
        _isDissolve = !_isDissolve;

        StartCoroutine(DissolveMethod());
    }
}
