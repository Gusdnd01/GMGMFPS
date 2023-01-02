using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GunCameraShake : MonoBehaviour
{

    public bool start = false;
    public AnimationCurve curve;
    [SerializeField] private float duration = 1;
    private Vector3 startPosition;

    private void Start()
    {
    }

    private void Update()
    {
        startPosition = transform.parent.position;

        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }
}
