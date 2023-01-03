using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public UIDissolve ui;

    private void Awake()
    {
        ui.IsDissolved = false;
    }
}

public static class TimeManager
{
    static float timeScale = 1;

    public static float TimeScale
    {
        get => timeScale;
        set => timeScale = value;
    }

    static float deltaTime;
    public static float DeltaTime { get { return Time.deltaTime * timeScale; } }
}
