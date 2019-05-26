using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class ImmVisManager : MonoBehaviour
{
    public ImmVisGameClient Client { get;  private set; }

    private void Awake()
    {
        Client = new ImmVisGameClient();
        Client.Initialize();
    }

    void OnApplicationQuit()
    {
        if (Client != null)
        {
            Client.Release();
            Client = null;
        }
    }

    [Obsolete("This method is obsolete, please use the Client property instead.")]
    public async Task<int> LoadDataSet(string filePath)
    {
        return await Client.OpenDatasetFromFile(filePath);
    }

    [Obsolete("This method is obsolete, please use the Client property instead.")]
    public async Task<List<DimensionInfo>> GetDimensions()
    {
        return await Client.GetDatasetDimensions();
    }

    [Obsolete("This method is obsolete, please use the Client property instead.")]
    public async Task<List<DimensionData>> RetrieveData(params string[] dimensionNames)
    {
        return await Client.GetDimensionsData(dimensionNames);
    }

    [Obsolete("This method is obsolete, please use the Client property instead.")]
    public async Task<List<bool>> RetrieveOutliersMapping(params string[] dimensionNames)
    {
        return await Client.GetOutliersMapping(dimensionNames);
    }

    [Obsolete("This method is obsolete, please use the Client property instead.")]
    public async Task<List<KMeansCentroid>> RetrieveKMeansCentroids(int numClusters, params string[] dimensionNames)
    {
        return await Client.GetKMeansCentroids(numClusters, dimensionNames);
    }

    [Obsolete("This method is obsolete, please use the Client property instead.")]
    public async Task<List<int>> RetrieveKMeansClusterMapping(int numClusters, params string[] dimensionNames)
    {
        return await Client.GetKMeansClusterMapping(numClusters, dimensionNames);
    }

    [Obsolete("This method is obsolete, please use the Client property instead.")]
    public async Task<List<Feature>> GetDescriptiveStatistics(string dimensionName)
    {
        return await Client.GetDimensionDescriptiveStatistics(dimensionName);
    }
}
