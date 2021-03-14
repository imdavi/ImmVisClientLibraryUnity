using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuKMeans : BaseScreen
{
    [SerializeField]
    private MainMenuManager mainMenuManager;

    [SerializeField]
    private TMP_Dropdown xDropdown;

    [SerializeField]
    private TMP_Dropdown yDropdown;

    [SerializeField]
    private TMP_Dropdown zDropdown;

    [SerializeField]
    private Slider centersSlider;

    protected override void OnShow(object data = null)
    {
        if (data != null && data is DatasetMetadata)
        {
            DatasetMetadata datasetMetadata = (DatasetMetadata)data;

            var columnsFromMandatoryFields = datasetMetadata.ColumnsInfo.Select(columnInfo => columnInfo.Column.ColumnName).ToList();

            xDropdown.options.Clear();
            yDropdown.options.Clear();
            zDropdown.options.Clear();

            xDropdown.AddOptions(columnsFromMandatoryFields);
            yDropdown.AddOptions(columnsFromMandatoryFields);
            zDropdown.AddOptions(columnsFromMandatoryFields);
        }
    }

    public void ClickedOnPlot()
    {
        var selectedOptions = new List<string> {
            xDropdown.options[xDropdown.value].text,
            yDropdown.options[yDropdown.value].text,
            zDropdown.options[zDropdown.value].text
        };

        var centersToDetect = (int)centersSlider.value;

        mainMenuManager?.RequestedToPlotKMeans(selectedOptions, centersToDetect);
    }
}
