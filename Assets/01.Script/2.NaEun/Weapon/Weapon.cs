using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Class")]
    [SerializeField] protected WeaponDataSo data;

    public WeaponDataSo Data { get { return data; } }

    public abstract void Attack(Action callbackAction);
    public abstract void Reload(Action callbackAction);
    public abstract void RightClick(Action callbackAction);

    protected void PlayAudio(AudioClip clip)
    {
        AudioManager.PlayAudioRandPitch(clip);
    }
}