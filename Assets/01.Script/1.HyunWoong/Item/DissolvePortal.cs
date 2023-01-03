using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvePortal : MonoBehaviour
{
    Material cubeMat;
    Material tourMat;

    float fade;

    [SerializeField]
    float fadeSpeed;

    public bool isDissolve;
    private void Awake()
    {
        cubeMat = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        tourMat = transform.GetChild(1).GetComponent<MeshRenderer>().material;
        tourMat.SetFloat("_Cutoff_Height", -2);
        cubeMat.SetFloat("_Cutoff_Height", -2);
    }

    private void Start()
    {
        StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve()
    {
        while (true)
        {
            if (isDissolve)
            {
                fade += Time.deltaTime * fadeSpeed;

                if (fade >= 2)
                {
                    fade = 2;
                    StopCoroutine(Dissolve());
                }

                cubeMat.SetFloat("_Cutoff_Height", fade);
                tourMat.SetFloat("_Cutoff_Height", fade);
            }
            yield return null;
        }
    }
}
