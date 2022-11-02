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
    [Header("OpenScene()�� ���̴� ��")]
    public string SceneName;

    [Header("OpenLevel()�� ���̴� ��")]
    public int level;
    public bool isOpenLevel;

    TextMeshProUGUI text;

    SceneLoader sceneLoader = new SceneLoader();

    [SerializeField] TextMeshProUGUI aa;
    private void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();
        if (isOpenLevel)
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = level.ToString();
        }
    }

    public void OpenScene()
    {

        SceneLoader.Instance.LoadScene(SceneName);
        //SceneManager.LoadScene(SceneName);
    }

    public void OpenLevel()
    {
        SceneLoader.Instance.LoadScene("Level " + level.ToString());

    }

    public void Quit()
    {
        Application.Quit();
    }
}
