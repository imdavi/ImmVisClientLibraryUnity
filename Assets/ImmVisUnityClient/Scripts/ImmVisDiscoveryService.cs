

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ImmVisDiscoveryService
{
    private const string MAGIC_ID = "U2bhY3XUOli9GgdUGs9ruxuXKpuj78Qi3zNT5IEkiQy5ex4UxqXZ5ZDAj9vkTyVz2GZiFXDS4bY5Ayve2HrAiB7G2jN7d5rskERyj3b5GeQAv1PYEOdD5sys";
    private const int DEFAULT_DISCOVERY_PORT = 5000;

    private const long DEFAULT_SEARCH_TIMEOUT = 10000;

    private CancellationToken CancellationToken { get; set; }

    public int Port { get; private set; }

    public ImmVisDiscoveryService(CancellationToken token, int port = DEFAULT_DISCOVERY_PORT)
    {
        Port = port;
    }

    public async Task<List<string>> SearchForAvailableServers(bool returnOnFirst = true, long searchTimeout = DEFAULT_SEARCH_TIMEOUT)
    {
        var udpClient = new UdpClient(Port);

        var availableServers = new List<string>();

        while (true)
        {
            try
            {
                CancellationToken.ThrowIfCancellationRequested();

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

                        if (magic == MAGIC_ID)
                        {
                            var ip = result.RemoteEndPoint.Address.ToString();
                            
                            availableServers.Add(ip);

                            if (returnOnFirst)
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

        udpClient.Close();
        udpClient.Dispose();

        return availableServers;
    }
}