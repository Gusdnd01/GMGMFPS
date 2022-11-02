using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunSound")]
public class GunSoundSetting : ScriptableObject
{
    [Header("GunSounds")]
    public List<AudioClip> shotSound = new List<AudioClip>();
    public List<AudioClip> reloadSound = new List<AudioClip>();
}
