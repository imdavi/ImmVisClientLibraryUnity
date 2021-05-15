using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CentersUpdateTextWithSliderValue : MonoBehaviour
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
        if (newValue == 0f)
        {
            valueTextComponent.text = "Automatic";
        }
        else
        {
            valueTextComponent.text = $"{(int)newValue}";
        }

    }
}
