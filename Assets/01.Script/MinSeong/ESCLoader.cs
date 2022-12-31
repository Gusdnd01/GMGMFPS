using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Build.Content;
#endif
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ESCLoader : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingPanel;

    [SerializeField] Button resumeButton;

     public GameObject OnePos;
    public GameObject TwoPos;
    [SerializeField] GameObject ThreePos;
    [SerializeField] GameObject FourPos;

    [Space]
    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    Slider masterAudioSlider;
    [SerializeField]
    Slider FXAudioSlider;
    [SerializeField]
    Slider BGMAudioSlider;

    public bool isMainMenu;

    public bool isPausing = false;
    public bool isInSetting = false;

    public bool isFullScreen = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }   

        audioMixer.SetFloat("Master", masterAudioSlider.value);
        audioMixer.SetFloat("BGM", FXAudioSlider.value);
        audioMixer.SetFloat("SFX", BGMAudioSlider.value);

        OnPanel();
    }

    public void PauseMenu()
    {
        isPausing = !isPausing;
    }

    public void SettingMenu()
    {
        isInSetting = !isInSetting;
    }

    public void OtherButton(string sceneName)
    {
        SceneLoader.Instance.LoadScene(sceneName);
    }

    private void OnPanel()
    {
        if (isPausing && !isInSetting && !isMainMenu)
        {
            LerpMove(pausePanel, TwoPos);
            LerpMove(settingPanel, ThreePos);
        }
        if (isPausing && isInSetting && !isMainMenu)
        {
            LerpMove(pausePanel, OnePos);
            LerpMove(settingPanel, TwoPos);
        }
        if (!isPausing && !isMainMenu)
        {
            LerpMove(pausePanel, ThreePos);
            LerpMove(settingPanel, FourPos);
            isInSetting = false;
        }


        if (isInSetting && isMainMenu)
        {
            LerpMove(settingPanel, TwoPos);
        }
        if (!isInSetting && isMainMenu)
        {
            LerpMove(settingPanel, ThreePos);
        }
    }

    public void ChangeScreenMode()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
        Debug.Log("fullScreen " + isFullScreen);
    }

    public void ChangeResolution(int index)
    {
        if (index == 0)
        {
            Debug.Log("1920 qkRna");
            Screen.SetResolution(1920, 1080, isFullScreen);
        }
        else if (index == 1)
        {
            Debug.Log("1366 qkRna");
            Screen.SetResolution(1366, 768, isFullScreen);
        }
        else if (index == 2)
        {
            Debug.Log("1024 qkRna");
            Screen.SetResolution(1024, 768, isFullScreen);
        }
    }

    public void LerpMove(GameObject obj, GameObject pos)
    {
        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(obj.GetComponent<RectTransform>().anchoredPosition,
            new Vector2(pos.GetComponent<RectTransform>().anchoredPosition.x,
            pos.GetComponent<RectTransform>().anchoredPosition.y), 0.15f);
    }
}
