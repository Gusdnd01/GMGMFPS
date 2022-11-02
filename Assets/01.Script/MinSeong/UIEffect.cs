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

    float lerpT = 0.7f;
    float curTime = 0;

    public bool isPointEnter;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => StartCoroutine(Scaling()));
        GetComponent<RectTransform>().localScale = from;
    }

    private void Update()
    {
        if (isPointEnter)
        {
            curTime += Time.deltaTime;
            if (curTime >= lerpT)
                curTime = lerpT;
            GetComponent<RectTransform>().localScale = Vector3.Lerp(mouseOn, from, curTime / lerpT);
        }
        else if (!isPointEnter)
        {
            curTime += Time.deltaTime;
            if (curTime >= lerpT)
                curTime = lerpT;
            GetComponent<RectTransform>().localScale = Vector3.Lerp(from, mouseOn, curTime / lerpT);
        }
        curTime = 0;
    }

    IEnumerator Scaling()
    {
        GetComponent<RectTransform>().localScale = to;
        yield return new WaitForSeconds(Time.fixedDeltaTime * 3);
        GetComponent<RectTransform>().localScale = from;
    }

    public void PointEnter()
    {
        isPointEnter = true;
    }

    public void PointExit()
    {
        isPointEnter = false;
    }
}
