using UnityEngine;
using UnityEngine.UI;
using NZLib.Utilities;

namespace NZLib.Utilities.CommunicationCore
{
    public sealed class SendMessageButton : MonoBehaviour
    {
        #region Getters

        private InputField _getInputField
        {
            get
            {
                if (_inputField == null)
                {
                    _inputField = GameObject.FindObjectOfType<InputField>();
                }
                return _inputField;
            }
        }

        #endregion

        /* ===================================================================================== */
        // Components

        private InputField _inputField;

        /* ===================================================================================== */
        // Base

        private void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessage();
            }
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Sends written message to Server.
        /// </summary>
        public void SendMessage ()
        {
            var t_client = GameObject.FindObjectOfType<TCPClient>();
            if (t_client)
            {
                t_client.EnqueueMessage(_getInputField.text);
                Clear();
                return;
            }
            Debug.LogWarning("Could not find any 'TCPClient' on scene to send the message.");            
        }

        /// <summary>
        /// Clears text field.
        /// </summary>
        private void Clear ()
        {
            _getInputField.text = "";
        }
    }
}

