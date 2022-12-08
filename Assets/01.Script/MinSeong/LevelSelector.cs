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

    SceneLoader sceneLoader;

    [Space]
    [SerializeField] bool isCantGoIn;
    [SerializeField] GameObject cantGoInCanvas;
    private void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();
        text = GetComponentInChildren<TextMeshProUGUI>();

        if (isCantGoIn)
            cantGoInCanvas.SetActive(false);
    }

    public void OpenScene(string SceneName)
    {
        if (isCantGoIn)
        {
            cantGoInCanvas.SetActive(true);
            return;
        }
        SceneLoader.Instance.LoadScene(SceneName);
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
