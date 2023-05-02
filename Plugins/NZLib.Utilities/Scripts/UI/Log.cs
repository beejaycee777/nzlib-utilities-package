using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using NZLib.Utilities.CommunicationCore;

namespace NZLib.Utilities
{
    /// <summary>
    /// Controls any behaviour related to the Log.
    /// </summary>
    public sealed class Log : MonoBehaviour
    {
        #region Constants

        private const string MESSAGE_COLOR = "<color=#989898>";
        private const string SERVER_MESSAGE_COLOR = "<color=#A94A69>";
        private const string CLIENT_MESSAGE_COLOR = "<color=#5069A4>";
        private const string ERROR_COLOR = "<color=#FF1400>";
        private const string ITALIC = "<i>";
        private const string END_ITALIC = "</i>";
        private const string END_COLOR = "</color>";
        private const string LINE_BREAK = "\n";

        #endregion

        #region Getters

        /// <summary>
        /// Returns Text component.
        /// </summary>
        private Text _getLogText
        {
            get
            {
                if (_text == null)
                {
                    _text = _getContent.GetComponentInChildren<Text>();
                }
                return _text;
            }
        }
        /// <summary>
        /// Returns Content's RectTransform.
        /// </summary>
        private RectTransform _getContent
        {
            get
            {
                if (_content == null)
                {
                    _content = GetComponentInChildren<ScrollRect>().content;
                }
                return _content;
            }
        }

        #endregion

        /* ===================================================================================== */
        // Components

        /// <summary>
        /// Text component.
        /// </summary>
        private Text _text;
        /// <summary>
        /// Content's RectTransform component.
        /// </summary>
        private RectTransform _content;

        /* ===================================================================================== */
        // Base

        private void Start ()
        {
            PrintNotification("Welcome! Please, feel free to send any message through the UI layout on the right.");

            var t_servers = GameObject.FindObjectsOfType<TCPServer>();
            for (var i = 0; i < t_servers.Length; ++i)
            {
                t_servers[i].OnMessage += HandleOnReceiveMessage;
            }
            var t_clients = GameObject.FindObjectsOfType<TCPClient>();
            for (var i = 0; i < t_clients.Length; ++i)
            {
                t_clients[i].OnMessage += HandleOnSendMessage;
            }
        }

        private void OnDestroy ()
        {
            var t_servers = GameObject.FindObjectsOfType<TCPServer>();
            for (var i = 0; i < t_servers.Length; ++i)
            {
                t_servers[i].OnMessage -= HandleOnReceiveMessage;
            }
            var t_clients = GameObject.FindObjectsOfType<TCPClient>();
            for (var i = 0; i < t_clients.Length; ++i)
            {
                t_clients[i].OnMessage -= HandleOnSendMessage;
            }
        }

        private void OnApplicationQuit ()
        {
            OnDestroy();
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Prints requested notification.
        /// </summary>
        public void PrintNotification (string notification)
        {
            _getLogText.text += LINE_BREAK + MESSAGE_COLOR + notification + END_COLOR;
            StartCoroutine(ReachBottom());
        }

        /// <summary>
        /// Prints a message received from client.
        /// </summary>
        public void PrintMessageReceived (string message, string client)
        {
            _getLogText.text += LINE_BREAK + ITALIC + CLIENT_MESSAGE_COLOR + "Message received from '" + client + "': " + END_COLOR + END_ITALIC + message;
            StartCoroutine(ReachBottom());
        }

        /// <summary>
        /// Prints a message sent to server.
        /// </summary>
        public void PrintMessageSent (string message)
        {
            _getLogText.text += LINE_BREAK + ITALIC + SERVER_MESSAGE_COLOR + "Message sent: " + END_COLOR + END_ITALIC + message;
            StartCoroutine(ReachBottom());
        }

        /// <summary>
        /// Prints an error.
        /// </summary>
        public void PrintError (string message)
        {
            _getLogText.text += ERROR_COLOR + message + END_COLOR + LINE_BREAK;
            StartCoroutine(ReachBottom());
        }

        /// <summary>
        /// Reach the lowest value of vertical scrollbar
        /// </summary>
        private IEnumerator ReachBottom ()
        {
            while (_getContent.anchoredPosition.y != 0)
            {
                Canvas.ForceUpdateCanvases();
                var t_position = _getContent.anchoredPosition;
                t_position.y = 0;
                _getContent.anchoredPosition = t_position;
                yield return null;
            }
        }

        private void HandleOnReceiveMessage (string msg)
        {
            var t_message = msg;
            var t_client = "Unknown";
            if (msg.Contains('#'))
            {
                t_message = msg.Split('#')[1];
                t_client = msg.Split('#')[0];
            }
            PrintMessageReceived(t_message, t_client);
        }

        private void HandleOnSendMessage (string msg)
        {
            var t_message = msg;
            var t_server = "Unknown";
            if (msg.Contains('#'))
            {
                t_message = msg.Split('#')[1];
                t_server = msg.Split('#')[0];
            }
            PrintMessageSent(t_message);
        }        
    }
}

