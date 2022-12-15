using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Playables;

public class Cinematic : MonoBehaviour
{
    [SerializeField]
    PlayableDirector playable;

    void Start()
    {
    }

    void Update()
    {
        if (playable.playableGraph.IsDone())
        {
            GetComponent<CinemachineVirtualCamera>().Priority = 5;
        }
    }
}
