using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGenerateDataset : BaseScreen
{
    [SerializeField]
    private MainMenuManager mainMenuManager;

    [SerializeField]
    private Slider columnsAmountSlider;

    [SerializeField]
    private Slider rowsAmountSlider;
    
    [SerializeField]
    private Slider centersAmountSlider;


    public void ClickedOnGenerateDataset()
    {
        int columnsAmount = (int)columnsAmountSlider.value;
        int rowsAmount = (int)rowsAmountSlider.value;
        int centersAmount = (int)centersAmountSlider.value;

        mainMenuManager.GenerateDataset(columnsAmount, rowsAmount, centersAmount);
    }
}
