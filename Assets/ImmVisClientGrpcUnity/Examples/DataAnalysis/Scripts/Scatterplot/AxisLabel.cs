using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AxisLabel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI labelAxisText;

    public void Show(string label)
    {
        transform.parent.gameObject.SetActive(true);
        labelAxisText.text = label;
    }

    public void Hide()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
