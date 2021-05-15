using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActions : BaseScreen
{
    [SerializeField]
    private ScreenManager screenManager;

    private DatasetMetadata datasetMetadata;

    protected override void OnShow(object data = null)
    {
        if (data != null && data is DatasetMetadata)
        {
            datasetMetadata = (DatasetMetadata)data;
        }
    }

    public void ClickedOnPlotDataset()
    {
        screenManager.ShowScreen("PlotDataset", datasetMetadata);
    }

    public void ClickedOnKMeans()
    {
        screenManager.ShowScreen("KMeans", datasetMetadata);
    }

    public void ClickedOnDescriptiveStatistics() 
    {
        screenManager.ShowScreen("DescriptiveStatistics", datasetMetadata);
    }
}
