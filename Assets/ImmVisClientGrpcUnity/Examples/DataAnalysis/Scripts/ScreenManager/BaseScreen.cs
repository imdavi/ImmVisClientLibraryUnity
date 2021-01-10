using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    public bool IsPopup = false;

    public void Show(object data = null)
    {
        gameObject.SetActive(true);
        OnShow(data);
    }

    protected virtual void OnShow(object data = null) { }

    public void Hide()
    {
        gameObject.SetActive(false);
        OnHide();
    }

    protected virtual void OnHide() { }
}
