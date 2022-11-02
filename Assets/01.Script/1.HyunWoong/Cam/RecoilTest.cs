using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilTest : MonoBehaviour
{
    [Header("회전관련 수치")]
    [SerializeField] private Transform cam;
    [SerializeField] private Vector2 camLimit = new Vector2(-45f, 45f);
    [SerializeField] private float senservity = 500f;
    [SerializeField] private AnimationCurve recoilControllCurve;
    [SerializeField] private Transform camAxis;
    private Vector2 recoil;



}
