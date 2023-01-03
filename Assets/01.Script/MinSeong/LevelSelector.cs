using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*using UnityEngine.*/

public class LevelSelector : MonoBehaviour
{
    [SerializeField] bool isLevelSelect;
    [SerializeField] string stageName;
    [SerializeField] int stageIndex;
    [SerializeField] GameObject textObj;
    [SerializeField]
    Sprite[] stageImages;

    TextMeshProUGUI text;
    Image image;

    SceneLoader sceneLoader;

    [Space]
    [SerializeField] bool isCantGoIn;
    [SerializeField] GameObject cantGoInCanvas;
    private void Start()
    {
        if (isLevelSelect)
        {
            sceneLoader = GetComponent<SceneLoader>();
            text = textObj.GetComponent<TextMeshProUGUI>();
            image = GetComponent<Image>();
                cantGoInCanvas.SetActive(false);

            if (isCantGoIn)
            {
                text.text = "??";
                image.sprite = stageImages[stageImages.Length - 1];
            }
            else
            {
                text.text = stageName;
                image.sprite = stageImages[stageIndex - 1];
            }
        }

    }

    public void OpenScene(string sceneName)
    {
        if (!isCantGoIn)
        {
            SceneManager.LoadScene(sceneName);
            return;
        }
        else
            cantGoInCanvas.SetActive(true);
        //SceneManager.LoadScene(SceneName);
    }

    public void CancelWindow(GameObject window)
    {
        window.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
