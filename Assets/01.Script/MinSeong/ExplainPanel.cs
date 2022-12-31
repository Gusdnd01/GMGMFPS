using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainPanel : MonoBehaviour
{
    [SerializeField] GameObject explainPanel;

    [SerializeField] GameObject OnePos;
    [SerializeField] GameObject TwoPos;
    public bool isExpain = false;
    
    ESCLoader loader;

    private void Start()
    {
        loader = GetComponent<ESCLoader>();
    }
    void Update()
    {
        if (isExpain)
        {
            LerpMove(explainPanel, TwoPos);
        }
        else
        {
            LerpMove(explainPanel, OnePos);
        }
    }

    public void Explain()
    {
        isExpain = !isExpain;
    }
    public void LerpMove(GameObject obj, GameObject pos)
    {
        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(obj.GetComponent<RectTransform>().anchoredPosition,
            new Vector2(pos.GetComponent<RectTransform>().anchoredPosition.x,
            pos.GetComponent<RectTransform>().anchoredPosition.y), 0.1f);
    }
}
