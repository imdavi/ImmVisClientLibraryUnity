using System;
using System.Collections;
using System.Collections.Generic;
using ImmVis.Grpc;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private ScreenManager screenManager;


    [SerializeField]
    public ImmVisGrpcClientManager immVisGrpcClientManager;

    [SerializeField]
    private ScatterplotBehaviour scatterplotBehaviour;

    void Start()
    {
        screenManager.ShowScreen("Loading", data: "Connecting to ImmVis server...");

        if (immVisGrpcClientManager != null)
        {
            immVisGrpcClientManager.ClientInitialized += () =>
            {
                screenManager.ShowScreen("Home");
            };
        }
    }

    public async void ClickedOnListAvailableDatasets()
    {
        screenManager.ShowScreen("Loading", data: "Loading available datasets...");

        try
        {
            var grpcClient = immVisGrpcClientManager.GrpcClient;
            var availableDatasets = await grpcClient.ListAvailableDatasetsAsync(new Empty());
            screenManager.ShowScreen("AvailableDatasets", data: availableDatasets);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
            screenManager.ShowScreen("Error");
        }
    }

    public void SelectedDataset(string clickedDataset)
    {
        LoadDatasetFromPath(clickedDataset);
    }

    public async void LoadDatasetFromPath(string datasetPath)
    {
        screenManager.ShowScreen("Loading", data: "Loading dataset...");

        try
        {
            var grpcClient = immVisGrpcClientManager.GrpcClient;
            var datasetMetadata = await grpcClient.LoadDatasetAsync(new LoadDatasetRequest()
            {
                DatasetPath = datasetPath
            });
            screenManager.ShowScreen("PlotDataset", data: datasetMetadata);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
            screenManager.ShowScreen("Error");
        }
    }

    public async void RequestedToPlot(List<string> selectedColumns)
    {
        screenManager.ShowScreen("Loading", data: "Plotting dataset...");

        try
        {
            var grpcClient = immVisGrpcClientManager.GrpcClient;
            var datasetToPlot = await grpcClient.GetNormalisedDatasetAsync(new GetNormalisedDatasetRequest()
            {
                ColumnsNames = { selectedColumns }
            });

            scatterplotBehaviour?.PlotData(datasetToPlot);

            screenManager.ShowScreen("DatasetPlotted");
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
            screenManager.ShowScreen("Error");
        }
    }

    public void ClickedOnGenerateDatasets()
    {
        screenManager.ShowScreen("GenerateDataset");
    }

    public async void GenerateDataset(int columnsAmount, int rowsAmount, int centersAmount)
    {
        screenManager.ShowScreen("Loading", data: "Loading dataset...");

        try
        {
            var grpcClient = immVisGrpcClientManager.GrpcClient;
            var datasetMetadata = await grpcClient.GenerateDatasetAsync(new GenerateDatasetRequest()
            {
                ColumnsAmount = columnsAmount,
                RowsAmount = rowsAmount,
                CentersAmount = centersAmount

            });
            screenManager.ShowScreen("PlotDataset", data: datasetMetadata);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
            screenManager.ShowScreen("Error");
        }
    }

    public void ClickedOnEnterDatasetsPath()
    {
        screenManager.ShowScreen("EnterPath");
    }

    public void ClickedOnReset()
    {
        screenManager.ClearNavigationStack();
        screenManager.ShowScreen("Home");

        scatterplotBehaviour.ResetScatterplot();
    }

    public void SubmitDatasetPath(string datasetPath)
    {
        LoadDatasetFromPath(datasetPath);
    }

    public void ClickedOnCancel()
    {
        screenManager.NavigateBack();
    }
}
