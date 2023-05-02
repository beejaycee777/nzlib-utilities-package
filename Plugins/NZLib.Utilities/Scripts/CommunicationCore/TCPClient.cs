using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace NZLib.Utilities.CommunicationCore
{
    /// <summary>
    /// TCP Client ready to send messages ONLY.
    /// </summary>
    public sealed class TCPClient : TCPAgent
    {
        internal class Data
        {
            public readonly string Name = "Local";
            public readonly string Ip = "127.0.0.1";
            public readonly string Mac = "A1B2C3D4E5F6";
            public readonly int Port = 5555;
            public readonly int Timeout = 3;

            public Data () { }
            public Data (string name, string ip, string mac, int port, int timeout)
            {
                Name = name;
                Ip = ip;
                Mac = mac;
                Port = port;
                Timeout = timeout;
            }
            public Data (string ip, string mac, int port)
            {
                Ip = ip;
                Mac = mac;
                Port = port;
            }
        }        

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Flag that sends contact or not with any message.
        /// </summary>
        private bool _sendContact = true;

        /* ===================================================================================== */
        // Components

        /// <summary>
        /// Server data.
        /// </summary>
        private Data _data;
        /// <summary>
        /// Handle to connected TCP server.
        /// </summary>
        private TcpClient _socketConnection;
        /// <summary>
        /// Background thread for workload.
        /// </summary>
        private Thread _thread;

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Sends a message to server.
        /// </summary>
        public void EnqueueMessage (string message)
        {
            if (!_dataReady)
            {
                Debug.LogWarning("Could not enqueue any message because data is not loaded yet.");
                return;
            }
            if (message.Length == 0)
            {
                Debug.LogWarning("Could not enqueue any message because length has to be more than 0.");
                return;
            }
            _thread = new Thread(new ThreadStart(delegate { SendMessageThroughNewThread(message); }));
            _thread.IsBackground = true;
            _thread.Start();
        }

        /// <summary>
        /// Loads data from IpConfigLoader.
        /// </summary>
        protected override IEnumerator LoadData ()
        {
            yield return new WaitUntil(() => IpConfigLoader.Instance.IsReady);
            _data = new Data
                (
                    IpConfigLoader.Instance.Config.Name,
                    IpConfigLoader.Instance.Config.ClientIp,
                    IpConfigLoader.Instance.Config.ClientMac,
                    IpConfigLoader.Instance.Config.ClientPort,
                    IpConfigLoader.Instance.Config.Timeout
                );
            _dataReady = true;
            Debug.Log("'" + _data.Name + "' client data has been loaded. [" + _data.Ip + ", " + _data.Port + "]");
        }

        /// <summary>
        /// Opens a new thread and sends a message through it.
        /// </summary>
        private void SendMessageThroughNewThread (string message)
        {
            try
            {
                var t_client = new TcpClient();
                var result = t_client.BeginConnect(_data.Ip, _data.Port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(_data.Timeout));
                if (!success)
                {
                    throw new Exception("Failed to connect.");
                }
                try
                {
                    if (_sendContact)
                    {
                        message = _data.Name + "#" + message;
                    }
                    // Gets a stream object for writing.
                    var stream = t_client.GetStream();
                    if (stream.CanWrite)
                    {
                        // Converts string message to byte array.                 
                        var t_clientMessageAsByteArray = Encoding.UTF8.GetBytes(message);
                        // Writes byte array to socketConnection stream.
                        stream.Write(t_clientMessageAsByteArray, 0, t_clientMessageAsByteArray.Length);
                        _msgQueue.Enqueue(message);
                    }
                    stream.Close();
                }
                catch (SocketException socketException)
                {
                    Debug.LogError("'" + _data.Name + "' could not send any message. Socket exception: " + socketException.ToString());
                }
                finally
                {
                    t_client.Close();
                }
            }
            catch (SocketException socketException)
            {
                Debug.LogError("'" + _data.Name + "' could not connect to server. Socket exception: " + socketException.ToString());
            }
        }
    }
}
