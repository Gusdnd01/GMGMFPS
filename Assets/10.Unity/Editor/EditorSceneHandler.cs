using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System;

public class EditorSceneHandler : MonoBehaviour
{
    #region Public
    [MenuItem("Scene/Public/Game")]
    public static void LoadGame()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Public/Game.unity");
    }
    [MenuItem("Scene/Public/Menu")]
    public static void LoadMenu()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Public/Menu.unity");
    }
    #endregion

    #region Dev
    [MenuItem("Scene/Dev/NaEun")]
    public static void LoadNaEun()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Dev/NaEun/NaEun.unity");
    }
    [MenuItem("Scene/Dev/HyunWoong")]
    public static void LoadHyunWoong()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Dev/HyunWoong/HyunWoong.unity");
    }
    [MenuItem("Scene/Dev/JunSeong")]
    public static void LoadJunSeong()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Dev/JunSeong/JunSeong.unity");
    }
    [MenuItem("Scene/Dev/SeolAh")]
    public static void LoadSeolAh()
    {
        EditorSceneManager.OpenScene("Assets/00.Scenes/Dev/SeolAh/SeolAh.unity");
    }
    #endregion
}
