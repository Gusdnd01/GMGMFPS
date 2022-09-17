using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public static class AudioManager
{
    public static void PlayAudio(AudioClip clip, float pitch = 1f, float volume = 1f)
    {
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(clip, pitch, volume);
    }
    public static void PlayAudioRandPitch(AudioClip clip, float pitch = 1f, float randValue = 0.1f, float volume = 1f)
    {
        PoolManager.Instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(clip, pitch + Random.Range(-randValue, randValue), volume);
    }
}
