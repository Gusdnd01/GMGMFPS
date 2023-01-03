using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameEndLineer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _timeline;


    bool end = false;

    private Transform _player = null;
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return _player;
        }
    }
    private void Awake()
    {
        _timeline = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        //end = false;
    }

    private void Update()
    {
        if(end)
        {
            _timeline.text = $"Goal";
        }
        else
        {
            _timeline.text = $" {Vector3.Distance(transform.position, Player.position).ToString("F2")} m Left";
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //FindObjectOfType<GameEnd>().GameClear();
            //end = true;
            _timeline.text = $"Goal";

        }
    }
}
