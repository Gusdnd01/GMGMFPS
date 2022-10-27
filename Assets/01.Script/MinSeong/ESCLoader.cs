using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCLoader : MonoBehaviour
{
    [SerializeField] GameObject panel;

    bool isPausing;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPausing = !isPausing;
            OnPanel();
        }
    }

    private void OnPanel()
    {
        panel.SetActive(isPausing);
    }
}
