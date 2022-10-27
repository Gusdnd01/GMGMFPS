using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunSoundSetting : ScriptableObject
{
    [Header("ShotSound")]
    public List<AudioClip> shotSound = new List<AudioClip>();
}
