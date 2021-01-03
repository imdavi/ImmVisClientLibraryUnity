using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : BaseScreen
{
    [SerializeField]
    private Text textComponent;

    [SerializeField]
    private Image barImage;

    public float Speed = 0.5f;

    protected override void OnShow(object data = null)
    {
        if(data != null && data is string)
        {
            textComponent.text = data.ToString();
        }
    }

    void Update()
    {
        var currentRotation = barImage.transform.rotation.eulerAngles;
        currentRotation.z += Speed * Time.deltaTime;
        barImage.transform.rotation = Quaternion.Euler(currentRotation);
    }
}
