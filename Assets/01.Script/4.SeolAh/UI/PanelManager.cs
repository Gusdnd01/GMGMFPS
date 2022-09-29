using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private Image settingPanel = null;
    [SerializeField] private Image weaponChoosePanel = null;
    private bool isSetting = false;
    private bool isWeaponCh = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isSetting = !isSetting;
            settingPanel.gameObject.SetActive(isSetting);
        }
         if (Input.GetKeyDown(KeyCode.B)) {
            isWeaponCh = !isWeaponCh;
            weaponChoosePanel.gameObject.SetActive(isWeaponCh);
        }
    }
}
