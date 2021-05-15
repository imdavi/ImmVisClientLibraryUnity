using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTextWithSliderValue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI valueTextComponent;

    [SerializeField]
    private Slider sliderComponent;

    void Start()
    {
        UpdateTextValue(sliderComponent.value);
    }

    public void UpdateTextValue(float newValue)
    {
        valueTextComponent.text = $"{(int)newValue}";
    }
}
