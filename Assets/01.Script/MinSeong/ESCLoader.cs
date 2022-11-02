using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESCLoader : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] Button resumeButton;

    [SerializeField] GameObject isPausePos;
    [SerializeField] GameObject isResumePos;

    public bool isPausing = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }

        OnPanel();
    }
    
    public void PauseMenu()
    {
        isPausing = !isPausing;
    }

    public void OtherButton(string sceneName)
    {
        SceneLoader.Instance.LoadScene(sceneName);
    }

    private void OnPanel()
    {
        if (isPausing)
        {
            panel.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(panel.GetComponent<RectTransform>().anchoredPosition,
                new Vector2(isPausePos.GetComponent<RectTransform>().anchoredPosition.x,
                isPausePos.GetComponent<RectTransform>().anchoredPosition.y), 0.1f);
        }
        else if (!isPausing)
        {
            panel.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(panel.GetComponent<RectTransform>().anchoredPosition,
                new Vector2(isResumePos.GetComponent<RectTransform>().anchoredPosition.x,
                isResumePos.GetComponent<RectTransform>().anchoredPosition.y), 0.1f);
        }
    }
}
