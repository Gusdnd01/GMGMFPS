using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] GameObject clearPanel;
    [SerializeField] GameObject overPanel;
    [SerializeField] GameObject onScreenPos;
    [SerializeField] GameObject outScreenPos;

    public bool isGameClear;
    public bool isGameEnd = false;
    void Start()
    {
        clearPanel.GetComponent<RectTransform>().anchoredPosition = outScreenPos.GetComponent<RectTransform>().anchoredPosition;
        overPanel.GetComponent<RectTransform>().anchoredPosition = outScreenPos.GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        if (isGameEnd)
        {
            if (isGameClear)
            {
                LerpMove(clearPanel, onScreenPos);
                LerpMove(overPanel, outScreenPos);
            }
            else if (!isGameClear)
            {
                LerpMove(overPanel, onScreenPos);
                LerpMove(clearPanel, outScreenPos);
            }
        }
    }

    private void LerpMove(GameObject obj, GameObject pos)
    {
        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(obj.GetComponent<RectTransform>().anchoredPosition,
            new Vector2(pos.GetComponent<RectTransform>().anchoredPosition.x,
            pos.GetComponent<RectTransform>().anchoredPosition.y), 0.1f);
    }

    public void gomenu()
    {
        isGameEnd = false;
        clearPanel.GetComponent<RectTransform>().anchoredPosition = outScreenPos.GetComponent<RectTransform>().anchoredPosition;
        overPanel.GetComponent<RectTransform>().anchoredPosition = outScreenPos.GetComponent<RectTransform>().anchoredPosition;
        SceneLoader.Instance.LoadScene("Main Menu");
    }
}
