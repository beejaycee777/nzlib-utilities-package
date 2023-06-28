using System.IO;
using UnityEngine;

namespace NZLib.Utilities.CommunicationCore
{
    /// <summary>
    /// Loads and manages Ip config.
    /// </summary>
    public class IpConfigLoader
    {
        #region Singleton

        /// <summary>
        /// Static instance for IpConfigLoader.
        /// </summary>
        private static IpConfigLoader _instance = new IpConfigLoader();
        public static IpConfigLoader Instance => _instance;

        #endregion

        /* ===================================================================================== */
        // Parameters

        public readonly bool IsReady = false;

        /* ===================================================================================== */
        // Components

        public readonly IpConfig Config;

        /* ===================================================================================== */
        // Base

        private IpConfigLoader ()
        {
            Config = new IpConfig();
            var t_readPath = Application.streamingAssetsPath + "/Data/ipconfig.json";
            using (var t_file = new StreamReader(t_readPath))
            {
                var t_jsonRead = t_file.ReadToEnd();
                JsonUtility.FromJsonOverwrite(t_jsonRead, Config);
            }
            IsReady = true;
        }
    }
}
