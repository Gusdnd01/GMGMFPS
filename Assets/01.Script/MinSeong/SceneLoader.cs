using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    protected static SceneLoader instance;

    UIDissolve canvases;

    [Header("FadeInOutCanvas")]
    [SerializeField]
    GameObject canvasGroup;

    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SceneLoader>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    Debug.LogError("SceneLoader is null");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadScene(string sceneName)
    {
        GameObject obj = Instantiate(canvasGroup);

        canvases = canvasGroup.GetComponentInChildren<UIDissolve>();
        canvases.material = canvases.GetComponent<Image>().material;
        StartCoroutine(Load(sceneName));
        canvases.IsDissolve();
    }

    private IEnumerator Load(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        yield return new WaitUntil(()=>op.isDone);
        LoadEnd();
    }

    private void LoadEnd(){
        canvases.IsDissolve();
    }
}