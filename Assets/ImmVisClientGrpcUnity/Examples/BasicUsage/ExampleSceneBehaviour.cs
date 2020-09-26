using System.Collections;
using System.Collections.Generic;
using System.Text;
using ImmVis.Grpc;
using UnityEngine;

public class ExampleSceneBehaviour : MonoBehaviour
{
    public ImmVisGrpcClientManager immVisGrpcClientManager;

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

            var datasetInfoStringBuilder = new StringBuilder();

            datasetInfoStringBuilder.AppendLine($"Dataset \"{datasetPath}\" ({datasetMetadata.RowsCount} rows x {datasetMetadata.ColumnsCount} columns)");
            datasetInfoStringBuilder.AppendLine("Colums:");
            foreach (var columnInfo in datasetMetadata.ColumnsInfo)
            {
                datasetInfoStringBuilder.AppendLine($"\t{columnInfo.Column.ColumnName} ({columnInfo.Column.Type})");
            }

            Debug.Log($"Loaded a dataset:\n{datasetInfoStringBuilder.ToString()}");
        }
        else
        {
            Debug.Log("There are no datasets available. :(");
        }
    }
}
