using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System;

public class EditorSceneHandler : EditorWindow
{
    SceneSetting ss;

    [MenuItem("HyunWoong/Window/SceneLoader")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(EditorSceneHandler));
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("SceneHandler", EditorStyles.boldLabel);
        GUILayout.Space(10);
        ss = (SceneSetting)EditorGUILayout.EnumPopup("SceneNames", ss);
        GUILayout.Space(10);

        if (GUILayout.Button("SceneMove"))
        {
            switch (ss)
            {
                case SceneSetting.Menu:
                    LoadMenu();
                    break;
                case SceneSetting.Game:
                    LoadGame();
                    break;
                case SceneSetting.Hyunwoong:
                    LoadHyunWoong();
                    break;
                case SceneSetting.JunSeong:
                    LoadJunSeong();
                    break ;
            }
        }
    }

    #region Public
    public static void LoadGame()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Public/Game.unity");
    }
    public static void LoadMenu()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Public/Menu.unity");
    }
    #endregion

    #region Dev

    public static void LoadHyunWoong()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Dev/HyunWoong/HyunWoong.unity");
    }

    public static void LoadJunSeong()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Dev/JunSeong/JunSeong.unity");
    }
    #endregion

    public enum SceneSetting
    {
        Menu,
        Game,
        Hyunwoong,
        JunSeong,
    }
}
