using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] GameObject clearPanel;
    [SerializeField] GameObject overPanel;
    [SerializeField] GameObject onScreenPos;
    [SerializeField] GameObject outScreenPos;

    public bool isGameEnd = false;
    void Start()
    {
        clearPanel.GetComponent<RectTransform>().anchoredPosition = outScreenPos.GetComponent<RectTransform>().anchoredPosition;
        overPanel.GetComponent<RectTransform>().anchoredPosition = onScreenPos.GetComponent<RectTransform>().anchoredPosition;
        overPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameEnd)
        {
            if (isGameEnd)
            {
                overPanel.SetActive(true);
            }
        }
    }

    public void GameClear()
    {
        isGameEnd = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        SceneManager.LoadScene("Main Menu");
    }
}
