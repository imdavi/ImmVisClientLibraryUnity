using System;
using System.Collections;
using System.Collections.Generic;
using ImmVis.Grpc;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public ImmVisGrpcClientManager immVisGrpcClientManager;

    public ScatterplotBehaviour scatterplotBehaviour;

    void Start()
    {
        if (immVisGrpcClientManager != null)
        {
            immVisGrpcClientManager.ClientInitialized += ClientIsReady;
        }
    }

    private async void ClientIsReady()
    {
        Debug.Log("The GrpcClient is ready to play!");

        var grpcClient = immVisGrpcClientManager.GrpcClient;

        var availableDatasets = await grpcClient.ListAvailableDatasetsAsync(new Empty());

        var datasetsPaths = availableDatasets.DatasetsPaths;

        if (datasetsPaths.Count > 0)
        {
            var datasetPath = datasetsPaths[0];

            var datasetMetadata = await grpcClient.LoadDatasetAsync(new LoadDatasetRequest()
            {
                DatasetPath = datasetPath
            });

            var pointsCount = datasetMetadata.RowsCount;

            var datasetToPlot = await grpcClient.GetNormalisedDatasetAsync(new GetNormalisedDatasetRequest());

            scatterplotBehaviour?.PlotData(datasetToPlot);
        }
    }
}
