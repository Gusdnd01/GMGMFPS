using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*using UnityEngine.*/

public class LevelSelector : MonoBehaviour
{
    [Header("OpenScene()에 쓰이는 것")]
    public string SceneName;

    [Header("OpenLevel()에 쓰이는 것")]
    public int level;
    public bool isOpenLevel;

    TextMeshProUGUI text;
    private void Start()
    {
        if (isOpenLevel)
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = level.ToString();
        }
    }

    public void OpenScene()
    {
        if (gameObject.name == "Start")
        {
            //SceneLoader.LoadScene("Level Selector");
            SceneManager.LoadScene(SceneName);
        }
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene("Level " + level.ToString());
    }

    public void Quit()
    {
        Application.Quit();
    }
}
