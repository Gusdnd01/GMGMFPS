using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Image hpSlider;
    public Image backSlider;

    private PlayerController playerController;

    private float curHp;

    private void Awake()
    {
        curHp = 100;
    }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        print($"hp : {curHp}");

        hpSlider.fillAmount = curHp;
        backSlider.fillAmount = Mathf.Lerp(backSlider.fillAmount, curHp, Time.deltaTime);
    }

    public void ModifyHp(float hp)
    {
        curHp = hp;
    }
}
