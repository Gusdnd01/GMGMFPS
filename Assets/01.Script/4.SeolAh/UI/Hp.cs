using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerController playerC = null;
    [SerializeField] private Slider hpSlider = null;

    float hp;
    float damage = 10f;
    float curHp = 100f;
    private void Awake() {
        hpSlider.value = playerData.hp;
        hp = playerData.hp;
    }
    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.E)) {
            // hpSlider.value -= playerC.currentHp;
            curHp -= damage;
            Debug.Log(curHp);
            hpSlider.value = Mathf.Lerp(hp, curHp, 0.1f);
        }
    }
}