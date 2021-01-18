using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatasetButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textComponent;
    
    private string dataset = "";

    public delegate void OnDatasetButtonClickDelegate(string dataset);

    public event OnDatasetButtonClickDelegate OnDatasetButtonClick;

    public void SetButtonDataset(string dataset)
    {
        this.dataset = dataset;
        textComponent.text = dataset;
    }

    public void OnClick()
    {
        if (OnDatasetButtonClick != null)
        {
            OnDatasetButtonClick.Invoke(dataset);
        }
    }
}
