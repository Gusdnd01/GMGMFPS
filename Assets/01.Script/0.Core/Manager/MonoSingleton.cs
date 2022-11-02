using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonoSingleton<T> : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            try
            {
                instance = GameObject.Find("GameManager").GetComponentInChildren<T>();
            }
            catch (NullReferenceException)
            {
                Debug.LogError($"Multiple instance is running{instance}");
            }

            return instance;
        }
    }
}
