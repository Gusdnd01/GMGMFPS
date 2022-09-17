using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootsStep : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    public void Play()
    {
        audioSource.Play();
    }
}
