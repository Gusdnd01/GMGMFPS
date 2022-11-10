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
    TextMeshProUGUI text;

    SceneLoader sceneLoader = new SceneLoader();

    [SerializeField] bool isCantGoIn;
    private void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OpenScene(string SceneName)
    {
        if (isCantGoIn)
        {

        }
        SceneLoader.Instance.LoadScene(SceneName);
        //SceneManager.LoadScene(SceneName);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
