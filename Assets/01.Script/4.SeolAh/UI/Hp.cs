using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private PlayerController playerC = null;
    [SerializeField] private Slider hpSlider = null;

    private void Awake() {
        hpSlider.value = playerData.hp;
    }
    private void Update() {
        float tlqkf = 10f;
        float tleos = 100f;
        if (Input.GetKeyDown(KeyCode.E)) {
            // hpSlider.value -= playerC.currentHp;
            tleos =- tlqkf;
            Debug.Log(tleos);
            hpSlider.value = Mathf.Lerp(tlqkf, tleos, 3f);
        }
    }
}