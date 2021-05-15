using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorScreen : BaseScreen
{
    [SerializeField]
    private TextMeshProUGUI textComponent;

    protected override void OnShow(object data = null)
    {
        if (data != null && data is Exception)
        {
            textComponent.text = ((Exception)data).Message;
        }
    }
}
