using System.Text;
using UnityEngine;

namespace NZLib.Utilities.DataManagement
{
    /// <summary>
    /// Manages HashTable behaviour.
    /// </summary>
    public sealed class HashTable
    {
        #region Getters

        /// <summary>
        /// Returns total number of entries.
        /// </summary>
        public int Count => _count;

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Full dictionary table.
        /// </summary>
        private string[] _table = null;
        /// <summary>
        /// Total number of entries.
        /// </summary>
        private int _count = 0;

        /* ===================================================================================== */
        // 

        public HashTable ()
        {
            _table = new string[1024];
        }

        public HashTable (int size)
        {
            _table = new string[size];
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Sets a new entry.
        /// </summary>
        /// <param name="key">New entry key.</param>
        /// <param name="val">New entry value.</param>
        public void Add (string key, string val)
        {
            _table[GetHash(key)] = val;
            _count++;
        }

        /// <summary>
        /// Gets an entry value for requested key.
        /// </summary>
        /// <param name="key">Requested entry key.</param>
        /// <returns>Entry value.</returns>
        public string Get (string key)
        {
            return _table[GetHash(key)];
        }

        /// <summary>
        /// Removes entry for the requested key.
        /// </summary>
        /// <param name="key">Key's entry to remove.</param>
        public void Remove (string key)
        {
            if (ContainsKey(key) == true)
            {
                _table[GetHash(key)] = "";
                _count--;
                return;
            }
            Debug.LogWarning("Dictionary does not contain any key like '" + key + "'.");
        }

        /// <summary>
        /// Returns TRUE or FALSED if table contains requested key or not.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>TRUE or FALSED if table contains requested key or not.</returns>
        public bool ContainsKey (string key)
        {
            if (_table[GetHash(key)] != "")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Clears whole table.
        /// </summary>
        public void Clear ()
        {
            _table = new string[_table.Length];
            _count = 0;
        }

        /// <summary>
        /// Gets hash for a given key.
        /// </summary>
        /// <param name="key">Requested key.</param>
        /// <returns>Hash created from key.</returns>
        private int GetHash (string key)
        {
            var t_hash = 0;
            for (var i = 0; i < key.Length; ++i)
            {
                t_hash += Encoding.ASCII.GetBytes(key[i].ToString())[0] * i;
            }
            t_hash %= _table.Length;
            if (_table[t_hash] != "")
            {
                Debug.LogError("A collision has been registered while setting a new entry into HashTable. Key: '" + key + "', Hash: '" + t_hash + "'.");
            }
            return t_hash;
        }
    }
}
