using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ImmVis.Discovery
{
    public class ImmVisDiscoveryManager : MonoBehaviour
    {
        private CancellationTokenSource cancellationTokenSource = null;

        public event DiscoveryFinished DiscoveryFinished;

        public int DiscoveryPort = 5000;

        public string MagicId = "U2bhY3XUOli9GgdUGs9ruxuXKpuj78Qi3zNT5IEkiQy5ex4UxqXZ5ZDAj9vkTyVz2GZiFXDS4bY5Ayve2HrAiB7G2jN7d5rskERyj3b5GeQAv1PYEOdD5sys";

        public bool IsRunning
        {
            get
            {
                return cancellationTokenSource != null;
            }
        }

        void OnApplicationQuit()
        {
            StopDiscovery();
        }

        public void StartDiscovery(int searchTimeoutInMilliseconds = 10000, bool shouldReturnOnFirstOccurrence = true)
        {
            if (cancellationTokenSource == null)
            {
                cancellationTokenSource = new CancellationTokenSource();

                if (searchTimeoutInMilliseconds > 0)
                {
                    cancellationTokenSource.CancelAfter(searchTimeoutInMilliseconds);
                }

                SearchForAvailableServers(cancellationTokenSource.Token, shouldReturnOnFirstOccurrence);
            }
            else
            {
                Debug.Log("It seems that Discovery service is already running...");
            }
        }

        public void StopDiscovery()
        {
            if (cancellationTokenSource != null)
            {
                if (!cancellationTokenSource.IsCancellationRequested)
                {
                    cancellationTokenSource.Cancel();
                }
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
        }

        private async void SearchForAvailableServers(CancellationToken cancelationToken, bool shouldReturnOnFirstOccurrence = true)
        {
            var udpClient = new UdpClient(DiscoveryPort);

            var availableServers = new List<string>();

            while (true)
            {
                try
                {
                    cancelationToken.ThrowIfCancellationRequested();

                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                    var result = await udpClient.ReceiveAsync();

                    byte[] receivedBytes = result.Buffer;

                    if (receivedBytes != null)
                    {
                        var data = Encoding.ASCII.GetString(receivedBytes);

                        Debug.Log("Message Received" + data.ToString());
                        Debug.Log("Address IP Sender" + result.RemoteEndPoint.ToString());

                        var splittedData = data.Split(':');

                        if (splittedData.Length == 2)
                        {
                            var magic = splittedData[0];

                            if (magic == MagicId)
                            {
                                var ip = result.RemoteEndPoint.Address.ToString();

                                availableServers.Add(ip);

                                if (shouldReturnOnFirstOccurrence)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                    break;
                }
            }

            DiscoveryFinished?.Invoke(availableServers);

            udpClient.Close();
            udpClient.Dispose();
        }
    }
}
