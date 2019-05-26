using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Google.Protobuf.Collections;

public class ImmVisGameClient
{
    private const string DEFAULT_TARGET = "127.0.0.1:50051";

    public string Target { get; private set; }

    public Channel Channel { get; private set; }

    public ImmVis.ImmVisClient Client { get; private set; }

    public ImmVisGameClient() : this(DEFAULT_TARGET) { }

    public ImmVisGameClient(string target)
    {
        Target = target;
    }

    public void Initialize()
    {
        Release();
        var target = Target != null && Target != "" ? Target : DEFAULT_TARGET;
        Channel = new Channel(target, ChannelCredentials.Insecure);
        Client = new ImmVis.ImmVisClient(Channel);
    }

    public void Release()
    {
        if (Client != null)
        {
            Client = null;
        }

        if (Channel != null)
        {
            Channel.ShutdownAsync().Wait();
            Channel = null;
        }
    }

    public async Task<int> OpenDatasetFromFile(string filePath)
    {
        var request = new OpenDatasetFileRequest()
        {
            FilePath = filePath
        };

        var response = await Client.OpenDatasetFileAsync(request);

        return response.ResponseCode;
    }

    public async Task<List<DimensionInfo>> GetDatasetDimensions()
    {
        var call = Client.GetDatasetDimensions(new Void());

        return await GetElementsFromResponseStream(call);
    }

    public async Task<List<Feature>> GetDimensionDescriptiveStatistics(string name)
    {
        var dimension = CreateDimension(name);

        var call = Client.GetDimensionDescriptiveStatistics(dimension);

        return await GetElementsFromResponseStream(call);
    }

    public async Task<DimensionInfo> GetDimensionInfo(string name)
    {
        var dimension = CreateDimension(name);

        return Client.GetDimensionInfo(dimension);
    }

    public async Task<List<Boolean>> GetOutliersMapping(params string[] dimensionsNames)
    {
        var call = Client.GetOutlierMapping();

        foreach (var dimensionName in dimensionsNames)
        {
            var dimension = CreateDimension(dimensionName);
            await call.RequestStream.WriteAsync(dimension);
        }

        await call.RequestStream.CompleteAsync();

        var dimensionData = await call.ResponseAsync;

        return dimensionData.Data.Select(element => Boolean.Parse(element)).ToList();
    }

    public async Task<List<KMeansCentroid>> GetKMeansCentroids(int numClusters, params string[] dimensionsNames)
    {
        var dimensions = dimensionsNames.Select(element => CreateDimension(element));

        var kMeansRequest = new KMeansRequest();

        kMeansRequest.NumClusters = numClusters;

        kMeansRequest.Dimensions.AddRange(dimensions);

        var call = Client.GetKMeansCentroids(kMeansRequest);

        return await GetElementsFromResponseStream(call);
    }

    public async Task<List<int>> GetKMeansClusterMapping(int numClusters, params string[] dimensionsNames)
    {
        var dimensions = dimensionsNames.Select(element => CreateDimension(element));

        var kMeansRequest = new KMeansRequest();

        kMeansRequest.NumClusters = numClusters;

        kMeansRequest.Dimensions.AddRange(dimensions);

        var dimensionData = await Client.GetKMeansClusterMappingAsync(kMeansRequest);

        return dimensionData.Data.Select(element => int.Parse(element)).ToList();
    }

    public async Task<List<DimensionData>> GetDimensionsData(params string[] dimensionsNames)
    {
        var call = Client.GetDimensionData();

        foreach (var dimensionName in dimensionsNames)
        {
            var dimension = CreateDimension(dimensionName);
            await call.RequestStream.WriteAsync(dimension);
        }

        await call.RequestStream.CompleteAsync();

        return await GetElementsFromResponseStream(call);
    }

    public async Task<List<DataRow>> GetDatasetValues()
    {
        var call = Client.GetDatasetValues(new Void());

        return await GetElementsFromResponseStream(call);
    }

    private Dimension CreateDimension(string name)
    {
        return new Dimension()
        {
            Name = name
        };
    }

    private async Task<List<T>> GetElementsFromResponseStream<T>(AsyncServerStreamingCall<T> call)
    {
        var elements = new List<T>();

        while (await call.ResponseStream.MoveNext())
        {
            T dimensionInfo = call.ResponseStream.Current;
            elements.Add(dimensionInfo);
        }

        return elements;
    }

    private async Task<List<U>> GetElementsFromResponseStream<T, U>(AsyncDuplexStreamingCall<T, U> call)
    {
        var elements = new List<U>();

        while (await call.ResponseStream.MoveNext())
        {
            U element = call.ResponseStream.Current;
            elements.Add(element);
        }

        return elements;
    }
}
