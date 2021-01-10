using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEnterPath : BaseScreen
{
    [SerializeField]
    private MainMenuManager mainMenuManager;

    [SerializeField]
    private InputField pathInputFieldComponent;

    [SerializeField]
    private Button submitButton;


    public void ClickedOnOkButton()
    {
        var datasetPath = pathInputFieldComponent.text;

        mainMenuManager.SubmitDatasetPath(datasetPath);
    }

    public void OnPathTextChanged(string newValue)
    {
        submitButton.interactable = !string.IsNullOrEmpty(pathInputFieldComponent.text.Trim());
    }
}
