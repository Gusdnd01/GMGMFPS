using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Image hpSlider;
    public Image backSlider;

    private float curHp;

    private void Awake()
    {
        curHp = 1;
    }

    private void FixedUpdate()
    {
        hpSlider.fillAmount = (float)curHp;
        backSlider.fillAmount = (float)Mathf.Lerp(backSlider.fillAmount, curHp, Time.deltaTime);
    }

    public void ModifyHp(float hp)
    {
        curHp = hp/100;
    }
}
