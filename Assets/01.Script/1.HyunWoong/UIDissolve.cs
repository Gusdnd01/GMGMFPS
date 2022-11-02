using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDissolve : MonoBehaviour
{
    [SerializeField]
    private bool _isDissolveOn = false;
    [SerializeField]
    private bool _isDissolveOff = false;

    float fade;

    Material material;

    private void Awake()
    {
        material = GetComponent<Image>().material;
        
    }

    private void Start()
    {
        fade = 1000;
        material.SetFloat("_Cutoff_Height", fade);
    }

    private void Update()
    {
        if (_isDissolveOn)
        {
            fade += 3;

            if (fade >= 1000)
            {
                fade = 1000;
                _isDissolveOn = false;
            }

            material.SetFloat("_Cutoff_Height", fade);
        }
        else if(_isDissolveOff)
        {
            fade -= 3;

            if (fade <= -1000)
            {
                fade = -1000;
                _isDissolveOff = false;
            }

            material.SetFloat("_Cutoff_Height", fade);
        }
    }

    public void DissolveOn()
    {
        _isDissolveOn = true;
    }

    public void DissolveOff()
    {
        _isDissolveOff = true;
    }
}
