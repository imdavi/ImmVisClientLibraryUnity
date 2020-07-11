using System;
using System.Collections;
using System.Collections.Generic;
using Grpc.Core;
using ImmVis.Discovery;
using UnityEngine;

namespace ImmVis.Grpc
{
    public class ImmVisGrpcClientManager : MonoBehaviour
    {
        public const int DefaultPort = 50051;

        public const string DefaultHost = "127.0.0.1";

        public ImmVisDiscoveryManager DiscoveryManager;

        private Channel Channel { get; set; }

        public ImmVisPandas.ImmVisPandasClient GrpcClient { get; private set; }

        public bool ShouldUseDiscoveryService = true;

        public event ClientInitialized ClientInitialized;

        public event ClientReleased ClientReleased;

        public bool IsReady
        {
            get
            {
                return Channel?.State == ChannelState.Ready;
            }
        }

        void Start()
        {
            if (ShouldUseDiscoveryService && DiscoveryManager != null)
            {
                DiscoveryManager.DiscoveryFinished += DiscoveryFinished;
                DiscoveryManager.StartDiscovery(shouldReturnOnFirstOccurrence: true);
                Debug.Log("Discovery has started!");
            }
            else
            {
                InitializeGrpcClient();
            }
        }

        void OnApplicationQuit()
        {
            ReleaseGrpcClient();
        }

        private void DiscoveryFinished(List<string> availableServersIps)
        {
            if (availableServersIps.Count > 0)
            {
                InitializeGrpcClient();
            }
        }

        private void InitializeGrpcClient(string host = DefaultHost, int port = DefaultPort)
        {
            if (GrpcClient != null)
            {
                ReleaseGrpcClient();
            }

            var target = $"{host}:{port}";
            Channel = new Channel(target, ChannelCredentials.Insecure, new List<ChannelOption> { new ChannelOption(ChannelOptions.MaxReceiveMessageLength, int.MaxValue) });
            GrpcClient = new ImmVisPandas.ImmVisPandasClient(Channel);
            ClientInitialized?.Invoke();
        }

        private void ReleaseGrpcClient()
        {
            if (GrpcClient != null)
            {
                GrpcClient = null;
            }

            if (Channel != null)
            {
                Channel.ShutdownAsync().Wait();
                Channel = null;
            }

            ClientReleased?.Invoke();
        }
    }
}
