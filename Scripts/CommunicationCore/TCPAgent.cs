using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace NZLib.Utilities.CommunicationCore
{
    public abstract class TCPAgent : MonoBehaviour
    {
        #region Events

        public UnityAction<string> OnMessage;

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Enqueued messages.
        /// </summary>
        protected Queue<string> _msgQueue = new Queue<string>();
        /// <summary>
        /// Flag with data status.
        /// </summary>
        protected bool _dataReady;

        /* ===================================================================================== */
        // Base

        protected void Start ()
        {
            StartCoroutine(LoadData());
        }

        protected void Update ()
        {
            CheckEnqueuedMessages();
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Loads data from IpConfigLoader.
        /// </summary>
        protected abstract IEnumerator LoadData ();

        /// <summary>
        /// Checks for any enqueued message.
        /// </summary>
        protected void CheckEnqueuedMessages ()
        {
            if (_msgQueue.Count == 0)
            {
                return;
            }
            foreach (var msg in _msgQueue)
            {
                OnMessage?.Invoke(msg);
            }
            _msgQueue.Clear();
        }
    }
}
