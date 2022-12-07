using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    public bool isGameEnd;
    public bool isClear;

    [SerializeField] GameObject screenPos;
    [SerializeField] GameObject underScreenPos;

    [SerializeField] GameObject gameClearPanel;
    [SerializeField] GameObject gameOverPanel;

    private void Start()
    {
        gameClearPanel.GetComponent<RectTransform>().anchoredPosition = underScreenPos.GetComponent<RectTransform>().anchoredPosition;
        gameOverPanel.GetComponent<RectTransform>().anchoredPosition = underScreenPos.GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        if (isGameEnd)
        {
            if (isClear)
            {
                LerpMove(gameClearPanel, screenPos);
            }
            else if (!isClear)
            {
                LerpMove(gameOverPanel, screenPos);
            }
        }
    }


    private void LerpMove(GameObject obj, GameObject pos)
    {
        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(obj.GetComponent<RectTransform>().anchoredPosition,
            new Vector2(pos.GetComponent<RectTransform>().anchoredPosition.x,
            pos.GetComponent<RectTransform>().anchoredPosition.y), 0.15f);
    }
}
