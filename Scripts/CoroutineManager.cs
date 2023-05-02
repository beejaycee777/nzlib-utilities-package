using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NZLib.Utilities
{
    /// <summary>
    /// Manages behaviours related to coroutines.
    /// </summary>
    public class CoroutineManager
    {
        internal class CoroutineData
        {
            public readonly long Id;
            public readonly MonoBehaviour Mono;
            public readonly IEnumerator Coroutine;

            public CoroutineData (long id, MonoBehaviour mono, IEnumerator coroutine)
            {
                Id = id;
                Mono = mono;
                Coroutine = coroutine;
            }

            public void Stop ()
            {
                Mono.StopCoroutine(Coroutine);
            }
        }

        /* ===================================================================================== */
        // Parameters

        private readonly static List<CoroutineData> _dataLog = new();

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Restarts given coroutine.
        /// </summary>
        public static void Restart (MonoBehaviour mono, IEnumerator coroutine)
        {
            mono.StopCoroutine(coroutine);
            mono.StartCoroutine(coroutine);
        }

        /// <summary>
        /// Registers given coroutine to gain control over and executes it.
        /// It may be useful when executing coroutines from other classes and wanting to stop them anytime.
        /// </summary>
        /// <param name="mono">MonoBehaviour in charge.</param>
        /// <param name="coroutine">Coroutine to register.</param>
        public static void Register (MonoBehaviour mono, IEnumerator coroutine)
        {
            _dataLog.Add(new(DateTime.Now.Ticks, mono, coroutine));
            mono.StartCoroutine(coroutine);
        }

        /// <summary>
        /// Registers given coroutine to gain control over and executes it.
        /// It may be useful when executing coroutines from other classes and wanting to stop them anytime.
        /// </summary>
        /// <param name="mono">MonoBehaviour in charge.</param>
        /// <param name="coroutine">Coroutines to register.</param>
        public static void Register (MonoBehaviour mono, IEnumerator[] coroutine)
        {
            for (var i = 0; i < coroutine.Length; i++)
            {
                Register(mono, coroutine[i]);
            }
        }

        /// <summary>
        /// Clears all coroutines registered from given MonoBehaviour.
        /// </summary>
        /// <param name="mono">MonoBehaviour to check.</param>
        public static void Clear (MonoBehaviour mono)
        {
            for (var i = 0; i < _dataLog.Count; i++)
            {
                if (_dataLog[i].Mono == mono)
                {
                    _dataLog[i].Stop();
                    _dataLog.RemoveAt(i);
                    --i;
                }
            }
        }
    }
}
