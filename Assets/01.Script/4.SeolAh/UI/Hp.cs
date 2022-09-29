using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private Slider hpSlider = null;

    private void Awake() {
        hpSlider.value = playerData.hp;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            // float num = 0.1f; //Damage 값을 받아온다
            // hpSlider.value =
        }
    }
}