using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] GameObject OnePos;
    [SerializeField] GameObject TwoPos;
    [SerializeField] GameObject ThreePos;
    [SerializeField] GameObject FourPos;

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
        if(isPausing && !isInSetting && !isMainMenu)
        {
            LerpMove(pausePanel, TwoPos);
            LerpMove(settingPanel, ThreePos);
        }
        if(isPausing && isInSetting && !isMainMenu)
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


        if(isInSetting && isMainMenu)
        {
            LerpMove(settingPanel, TwoPos);
        }
        if(!isInSetting && isMainMenu)
        {
            LerpMove(settingPanel, ThreePos);
        }
    }

    public void ChangeScreenMode()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void ChangeResolution(int index)
    {
        if (index == 0)
        {
            Screen.SetResolution(1920, 1080, isFullScreen);
        }
        else if (index == 1)
        {
            Screen.SetResolution(1366, 768, isFullScreen);
            Debug.Log("qkRna");
        }
        else if (index == 2)
        {
            Debug.Log("qkRna");
            Screen.SetResolution(1024, 768, isFullScreen);
        }
    }

    private void LerpMove(GameObject obj, GameObject pos)
    {
        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(obj.GetComponent<RectTransform>().anchoredPosition,
            new Vector2(pos.GetComponent<RectTransform>().anchoredPosition.x,
            pos.GetComponent<RectTransform>().anchoredPosition.y), 0.1f);
    }
}
