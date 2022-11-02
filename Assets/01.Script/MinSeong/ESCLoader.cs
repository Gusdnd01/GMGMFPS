using System.Collections;
using System.Collections.Generic;
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

    public bool isPausing = false;
    public bool isInSetting = false;
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
        if(isPausing && !isInSetting)
        {
            LerpMove(pausePanel, TwoPos);
            LerpMove(settingPanel, ThreePos);
        }
        if(isPausing && isInSetting)
        {
            LerpMove(pausePanel, OnePos);
            LerpMove(settingPanel, TwoPos);
        }
        if (!isPausing)
        {
            LerpMove(pausePanel, ThreePos);
            LerpMove(settingPanel, FourPos);
            isInSetting = false;
        }
    }

    private void LerpMove(GameObject obj, GameObject pos)
    {
        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(obj.GetComponent<RectTransform>().anchoredPosition,
            new Vector2(pos.GetComponent<RectTransform>().anchoredPosition.x,
            pos.GetComponent<RectTransform>().anchoredPosition.y), 0.1f);
    }
}
