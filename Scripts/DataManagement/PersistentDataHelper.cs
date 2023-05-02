using UnityEngine;
using System.IO;

namespace NZLib.Utilities.DataManagement
{
    /// <summary>
    /// Manages PersistentDataHelper behaviour.
    /// </summary>
    public sealed class PersistentDataHelper
    {
        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Loads requested data type from given path. If it doesn't exist yet, it does create it.
        /// </summary>
        /// <typeparam name="T">Data type.</typeparam>
        /// <param name="data">Empty data class to be used.</param>
        /// <param name="folder">Folder name. If there is more than one, separate them with '/'.</param>
        /// <param name="file">File name.</param>
        /// <param name="extension">File extension.</param>
        /// <returns>Loaded data.</returns>
        public static T Load<T> (T data, string folder, string file, string extension)
        {;
            var t_filePath = Path.Combine(Application.persistentDataPath, folder, file + extension);
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, folder)))
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, folder));
                Debug.Log("Folder '" + Path.Combine(Application.persistentDataPath, folder + "' has been created."));
            }
            if (!File.Exists(t_filePath))
            {
                var t_newFile = File.Create(t_filePath);
                t_newFile.Close();
                Save(data, folder, file, extension);
            }
            else
            {
                var t_jsonRead = File.ReadAllText(t_filePath);
                JsonUtility.FromJsonOverwrite(t_jsonRead, data);
                Debug.Log("Loading data from '" + t_filePath + "'.");
            }
            return data;
        }

        /// <summary>
        /// Saves given data type to requested path.
        /// </summary>
        /// <typeparam name="T">Data type.</typeparam>
        /// <param name="data">Data class to save.</param>
        /// <param name="folder">Folder name. If there is more than one, separate them with '/'.</param>
        /// <param name="file">File name.</param>
        /// <param name="extension">File extension.</param>
        public static void Save<T> (T data, string folder, string file, string extension)
        {
            var t_filePath = Path.Combine(Application.persistentDataPath, folder, file + extension);
            var t_jsonWrite = JsonUtility.ToJson(data, true);
            File.WriteAllText(t_filePath, t_jsonWrite);
            Debug.Log("Saving data to '" + t_filePath + "'.");
        }
    }
}
