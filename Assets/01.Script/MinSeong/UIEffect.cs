using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEffect : MonoBehaviour
{
    public Vector3 from = Vector3.one;
    public Vector3 to = Vector3.one * 0.95f;
    public Vector3 mouseOn = Vector3.one * 1.2f;

    float lerpT = 0.1f;

    public bool isPointEnter;

    void Start()
    {
        RectTransform originSize = GetComponent<RectTransform>();
    }

    private void Update()
    {

    }

    public void PointEnter()
    {
        GetComponent<RectTransform>().localScale = Vector3.Lerp(from, mouseOn, lerpT);
    }

    public void PointExit()
    {
        GetComponent<RectTransform>().localScale = Vector3.Lerp(mouseOn, from, lerpT);
        GetComponent<RectTransform>().localScale = from;
    }
}
