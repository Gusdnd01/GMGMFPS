using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class SlideLevels : MonoBehaviour
{
    public GameObject scrollbar;
    public RectTransform rect;
    float scrollPos = 0;
    float[] pos;

    float rectX;
    float rectY;

    private void Start()
    {
        rectX = rect.sizeDelta.x;
        rectY = rect.sizeDelta.y;
    }

    private void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if(scrollPos < pos[i] + (distance/2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = 
                    Vector2.Lerp(transform.GetChild(i).GetComponent<RectTransform>().sizeDelta, 
                    new Vector2(rectX, rectY), 0.1f);
                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).GetComponent<RectTransform>().sizeDelta = 
                            Vector2.Lerp(transform.GetChild(a).GetComponent<RectTransform>().sizeDelta, 
                            new Vector2(rectX * 0.8f, rectY * 0.8f), 0.1f);
                    }
                }
            }
        }
    }
}
