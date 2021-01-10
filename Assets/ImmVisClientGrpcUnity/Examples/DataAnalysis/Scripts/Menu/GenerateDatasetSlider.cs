using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateDatasetSlider : MonoBehaviour
{
    [SerializeField]
    private Text valueTextComponent;

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
