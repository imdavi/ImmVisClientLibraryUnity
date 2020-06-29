using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.EventSystems;

public class ImmVisManager : MonoBehaviour
{
    private ImmVisDiscoveryService DiscoveryService { get; set; }

    internal void RegisterToBroadcast(GameObject gameObject)
    {
        gameObject.transform.parent = this.gameObject.transform;
    }

    public ImmVisGameClient Client { get; private set; }

    private const string ImmVisReadyBroadcastMethodName = "OnImmVisReady";

    public bool AutoDiscoverServer = true;

    public String Host = ImmVisGameClient.DefaultHost;

    public int Port = ImmVisGameClient.DefaultPort;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private async void Awake()
    {
        if (AutoDiscoverServer)
        {
            InitializeWithAutoDiscovery();
        }
        else
        {
            InitializeGameClient(Host, Port);
        }
    }

    private async void InitializeWithAutoDiscovery()
    {
        DiscoveryService = new ImmVisDiscoveryService(cts.Token);

        var servers = await DiscoveryService.SearchForAvailableServers();

        if (servers.Count > 0)
        {
            InitializeGameClient(servers[0]);
        }
        else
        {
            Debug.Log("Could not find any available ImmVis servers... Did you forget to start something?");
        }
    }

    private void InitializeGameClient(string host = ImmVisGameClient.DefaultTarget, int port = ImmVisGameClient.DefaultPort)
    {
        Client = new ImmVisGameClient(host, port);
        Client.Initialize();
        BroadcastMessage(ImmVisReadyBroadcastMethodName);
    }
    void OnApplicationQuit()
    {
        if (Client != null)
        {
            Client.Release();
            Client = null;
        }

        cts.Cancel();

        if(DiscoveryService != null) 
        {
            DiscoveryService = null;
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

    public interface ImmVisEventTarget 
    {
        void OnImmVisReady();
    }
}
