using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Slider mouse = null;
    [SerializeField] private float value = 0f;

    private void Update()
    {
        OnValuesChanged();
    }
    private void OnValuesChanged()
    {
        value = mouse.value;
    }
}