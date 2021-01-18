using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlotDataset : BaseScreen
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
    private TMP_Dropdown sizeDropdown;

    [SerializeField]
    private TMP_Dropdown colorDropdown;

    protected override void OnShow(object data = null)
    {
        if (data != null && data is DatasetMetadata)
        {
            DatasetMetadata datasetMetadata = (DatasetMetadata)data;

            var columnsFromMandatoryFields = datasetMetadata.ColumnsInfo.Select(columnInfo => columnInfo.Column.ColumnName).ToList();

            var columnsFromNonMandatoryFields = new List<string> { "None" };
            columnsFromNonMandatoryFields.AddRange(columnsFromMandatoryFields);

            xDropdown.options.Clear();
            yDropdown.options.Clear();
            zDropdown.options.Clear();
            sizeDropdown.options.Clear();
            colorDropdown.options.Clear();

            xDropdown.AddOptions(columnsFromMandatoryFields);
            yDropdown.AddOptions(columnsFromMandatoryFields);
            zDropdown.AddOptions(columnsFromMandatoryFields);

            // Size and colors are not mandatory
            sizeDropdown.AddOptions(columnsFromNonMandatoryFields);
            colorDropdown.AddOptions(columnsFromNonMandatoryFields);
        }
    }

    public void ClickedOnPlot()
    {
        var selectedOptions = new List<string> {
            xDropdown.options[xDropdown.value].text,
            yDropdown.options[yDropdown.value].text,
            zDropdown.options[zDropdown.value].text
        };
        
        if(sizeDropdown.value != 0)
        {
            selectedOptions.Add(sizeDropdown.options[sizeDropdown.value].text);
        }

        if(colorDropdown.value != 0)
        {
            selectedOptions.Add(colorDropdown.options[colorDropdown.value].text);
        }

        mainMenuManager?.RequestedToPlot(selectedOptions);
    }
}
