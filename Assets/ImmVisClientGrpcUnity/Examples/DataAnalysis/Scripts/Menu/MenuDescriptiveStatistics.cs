using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Protobuf.Collections;
using TMPro;
using UnityEngine;

public class MenuDescriptiveStatistics : BaseScreen
{
    private DatasetMetadata datasetMetadata;


    [SerializeField]
    private TMP_Dropdown columnSelectorDropdown;

    [SerializeField]
    private TextMeshProUGUI rowsCountText;

    [SerializeField]
    private TextMeshProUGUI columnsCountText;


    [SerializeField]
    private TextMeshProUGUI featuresText;

    private RepeatedField<ColumnInfo> ColumnsInfo
    {
        get
        {
            return datasetMetadata.ColumnsInfo;
        }
    }

    protected override void OnShow(object data = null)
    {
        if (data != null && data is DatasetMetadata)
        {
            datasetMetadata = (DatasetMetadata)data;
            DisplayDatasetMetadata();
        }
    }

    private void DisplayDatasetMetadata()
    {
        rowsCountText.text = $"{datasetMetadata.RowsCount}";
        columnsCountText.text = $"{datasetMetadata.ColumnsCount}";

        columnSelectorDropdown.ClearOptions();
        var columns = datasetMetadata.ColumnsInfo.Select(columnInfo => columnInfo.Column.ColumnName).ToList();
        columnSelectorDropdown.AddOptions(columns);

        DisplayCurrentSelection();
    }

    public void DisplayCurrentSelection()
    {
        var selectedIndex = columnSelectorDropdown.value;

        if (selectedIndex >= 0 && selectedIndex < ColumnsInfo.Count)
        {
            var columnInfo = ColumnsInfo[selectedIndex];
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Type: {columnInfo.Column.Type}");

            foreach(var feature in columnInfo.DescriptiveStatisticsFeatures)
            {
                sb.AppendLine($"{feature.Name}:\t{feature.Value}");
            }

            featuresText.text = sb.ToString();
        }
        else
        {
            featuresText.text = "";
        }
    }
}
