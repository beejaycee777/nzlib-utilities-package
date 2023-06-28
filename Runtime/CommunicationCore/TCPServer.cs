using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace NZLib.Utilities.CommunicationCore
{
    /// <summary>
    /// TCP Server ready to receive messages from multiple clients.
    /// </summary>
    public sealed class TCPServer : TCPAgent
    {
        internal class Data
        {
            public readonly string Name = "Local";
            public readonly string Ip = "127.0.0.1";
            public readonly int Port = 3333;
            public readonly int Amount = 3;

            public Data () { }
            public Data (string server, string ip, int port, int amount)
            {
                Name = server;
                Ip = ip;
                Port = port;
                Amount = amount;
            }
            public Data (string ip, int port)
            {
                Ip = ip;
                Port = port;
            }
        }   

        /* ===================================================================================== */
        // Components

        /// <summary>
        /// Server data.
        /// </summary>
        private Data _data;
        /// <summary>
        /// Listens for incomming TCP connection requests.
        /// </summary>
        private TcpListener _tcpListener;
        /// <summary>
        /// Handle to connected TCP client.
        /// </summary>
        private TcpClient _tcpClient;
        /// <summary>
        /// Array of threads running at time.
        /// </summary>
        private Thread[] _threads;

        /* ===================================================================================== */
        // Base

        private new void Start ()
        {
            base.Start();
            StartCoroutine(EnableTCPListener());
        }

        private void OnDestroy ()
        {
            _tcpListener?.Stop();
        }

        private void OnApplicationQuit ()
        {
            _tcpListener?.Stop();
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Loads data from IpConfigLoader.
        /// </summary>
        protected override IEnumerator LoadData ()
        {
            yield return new WaitUntil(() => IpConfigLoader.Instance.IsReady);
            _data = new Data
                (
                    IpConfigLoader.Instance.Config.Name,
                    IpConfigLoader.Instance.Config.ServerIp,
                    IpConfigLoader.Instance.Config.ServerPort,
                    IpConfigLoader.Instance.Config.ServerAmount
                );
            _dataReady = true;
            Debug.Log("'" + _data.Name + "' server data has been loaded. [" + _data.Ip + ", " + _data.Port + "]");
        }

        /// <summary>
        /// Starts TCP Listener after IpConfig is done.
        /// </summary>
        private IEnumerator EnableTCPListener ()
        {
            yield return new WaitUntil(() => _dataReady);
            try
            {
                _tcpListener = new TcpListener(IPAddress.Parse(_data.Ip), _data.Port);
                _tcpListener.Start();
                OpenThreads(_data.Amount);
                Debug.Log("'" + _data.Name + "' is listening for incomming messages.");
            }
            catch
            {
                Debug.LogWarning("'" + _data.Name + "' could not start listening.");
            }
        }

        /// <summary>
        /// Opens as many threads as requested.
        /// </summary>
        private void OpenThreads (int amount)
        {
            _threads = new Thread[amount];
            for (var i = 0; i < amount; i++)
            {
                _threads[i] = new Thread(new ThreadStart(Listen));
                _threads[i].IsBackground = true;
                _threads[i].Start();
            }
            Debug.Log("Opened " + amount + " thread(s) on '" + _data.Name + "'.");
        }

        /// <summary>
        /// Listens for incomming messages.
        /// </summary>
        private void Listen ()
        {
            try
            {
                var t_bytes = new Byte[1024];
                while (true)
                {
                    using (_tcpClient = _tcpListener.AcceptTcpClient())
                    {
                        // Gets a stream object for reading.
                        using (var t_stream = _tcpClient.GetStream())
                        {
                            var t_length = 0;
                            // Reads incomming stream into byte array.			
                            while ((t_length = t_stream.Read(t_bytes, 0, t_bytes.Length)) != 0)
                            {
                                var t_incommingData = new byte[t_length];
                                Array.Copy(t_bytes, 0, t_incommingData, 0, t_length);
                                // Converts byte array to string message.
                                var t_clientMessage = Encoding.UTF8.GetString(t_incommingData);
                                if (t_clientMessage != null && t_clientMessage != "")
                                {
                                    _msgQueue.Enqueue(t_clientMessage);
                                }
                            }
                        }
                    }
                }
            }
            catch (SocketException socketException)
            {
                Debug.LogError("'" + _data.Name + "' could not receive any message. Socket exception: " + socketException.ToString());
            }
        }        
    }
}
