using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
using NZLib.Utilities.DataManagement;

namespace NZLib.Utilities.LanguageCore
{
    /// <summary>
    /// Manages all languages.
    /// </summary>
    public class LanguageController
    {
        internal class LanguageConfig
        {
            public string Default = "";
            public string Selected = "";
        }

        #region Singleton

        private static readonly LanguageController _instance = new LanguageController();
        public static LanguageController Instance => _instance;

        #endregion

        #region Events

        /// <summary>
        /// Event that triggers every time that Language gets changed.
        /// </summary>
        public UnityAction OnLanguageChange;

        #endregion

        #region Constants

        private const string DATA_FOLDER = "data";
        private const string DATA_FILE = "lang";
        private const string DATA_FILE_EXTENSION = ".json";

        #endregion

        #region Getters

        public Language GetLanguage => _language;

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Contains all languages found in 'Resources/Languages' folder.
        /// </summary>
        private readonly List<Language> _languages = new();
        /// <summary>
        /// Current Language being used on execution.
        /// </summary>
        private Language _language;

        /* ===================================================================================== */
        // Base

        private LanguageController ()
        {
            var t_files = Resources.LoadAll<TextAsset>("Languages");
            for (var i = 0; i < t_files.Length; ++i)
            {
                _languages.Add(ReadLanguageFile(t_files[i].name));
            }
            LoadData();
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Sets current Language based on a requested keyword.
        /// </summary>
        public void SetLanguage (string key)
        {
            for (var i = 0; i < _languages.Count; ++i)
            {
                if (_languages[i].Key == key)
                {
                    SetLanguage(_languages[i]);
                    return;
                }
            }
            Debug.LogWarning("Requested language key '" + key + "' does not exist in any XML document. Check your XML headers and try again.");
        }

        /// <summary>
        /// Sets current Language based on a requested system language.
        /// </summary>
        public void SetLanguage (SystemLanguage language)
        {
            for (var i = 0; i < _languages.Count; ++i)
            {
                if (_languages[i].SystemLanguage == language)
                {
                    SetLanguage(_languages[i]);
                    return;
                }
            }
            Debug.LogWarning("Requested system language '" + language + "' does not exist in any XML document. Check your XML headers and try again.");
        }

        /// <summary>
        /// Gets a word or expression value for a given keyword found in the current language dictionary.
        /// </summary>
        public string GetDictionaryValue (string key)
        {
            if (_language != null)
            {
                if (_language.Dictionary != null)
                {
                    if (_language.Dictionary.ContainsKey(key))
                    {
                        return _language.Dictionary[key];
                    }
                    Debug.LogWarning("Requested key " + key + " has not been found on current language dictionary.");
                }
                Debug.LogError("Dictionary has not been created.");
            }
            else
            {
                Debug.LogError("Language has not been set.");
            }
            return "";
        }

        /// <summary>
        /// Sets current Language based on a requested Language class.
        /// </summary>
        private void SetLanguage (Language language)
        {
            _language = language;
            OnLanguageChange?.Invoke();

            var t_config = new LanguageConfig()
            {
                Selected = language.Key
            };
            PersistentDataHelper.Save(t_config, DATA_FOLDER, DATA_FILE, DATA_FILE_EXTENSION);
            Debug.Log("Language has been set to '" + language.Label + "'.");
        }

        /// <summary>
        /// Reads an XML Language file from 'Resources/Languages' folder and extracts all its information.
        /// </summary>
        private Language ReadLanguageFile (string key)
        {
            var t_languageFileText = Resources.Load<TextAsset>("Languages/" + key);
            var t_xmlDocument = XDocument.Parse(t_languageFileText.text);
            var t_dictionary = new Dictionary<string, string>();
            foreach (var t_xmlElement in t_xmlDocument.Element("language").Elements())
            {
                try
                {
                    var t_newKey = t_xmlElement.Attribute(XName.Get("key")).Value;
                    var t_newValue = t_xmlElement.Attribute(XName.Get("value")).Value;
                    t_dictionary.Add(t_newKey, t_newValue);
                }
                catch { }
            }

            var t_key = t_xmlDocument.Element("language").Attribute(XName.Get("key")).Value;
            var t_label = t_xmlDocument.Element("language").Attribute(XName.Get("value")).Value;
            return new Language(t_key, t_label, t_dictionary);
        }

        /// <summary>
        /// Loads data and sets language (previously selected or default). Once per execution.
        /// </summary>
        private void LoadData ()
        {
            var t_langConfig = GetConfig();
            // Loads LanguageConfig class from persistent data path.
            t_langConfig = PersistentDataHelper.Load(t_langConfig, DATA_FOLDER, DATA_FILE, DATA_FILE_EXTENSION);
            // If there isn't any selected language yet, it sets system's by default and saves it.
            if (t_langConfig.Selected == "")
            {
                t_langConfig.Selected = SystemLanguageToString();
                PersistentDataHelper.Save(t_langConfig, DATA_FOLDER, DATA_FILE, DATA_FILE_EXTENSION);
            }
            SetLanguage(t_langConfig.Selected);
        }

        /// <summary>
        /// Loads a basic LanguageConfig class based on StreamingAssets json file.
        /// </summary>
        /// <returns>Configuration found in StreamingAssets file.</returns>
        private LanguageConfig GetConfig ()
        {
            var t_langConfig = new LanguageConfig();
            var t_filePath = Application.streamingAssetsPath + "/Data/lang.json";
            using (var t_file = new StreamReader(t_filePath))
            {
                var t_jsonRead = t_file.ReadToEnd();
                JsonUtility.FromJsonOverwrite(t_jsonRead, t_langConfig);
                t_file.Close();
            }
            return t_langConfig;
        }

        /// <summary>
        /// Gets current system language to string key.
        /// </summary>
        /// <returns>Language key string.</returns>
        private string SystemLanguageToString ()
        {
            for (var i = 0; i < _languages.Count; ++i)
            {
                if (_languages[i].SystemLanguage == Application.systemLanguage)
                {
                    return _languages[i].Key;
                }
            }
            // If system language is not contained, proceeds to set the default one from 'StreamingAssets' folder.
            Debug.LogWarning("System language doesn't have any reference in list. Default language will be the one in 'StreamingAssets' folder.");
            var t_default = GetConfig().Default;
            for (var i = 0; i < _languages.Count; ++i)
            {
                if (_languages[i].Key == t_default)
                {
                    return t_default;
                }
            }
            // If default language found in 'StreamingAssets' folder is not contained, proceeds to set 'English' as default.
            Debug.LogWarning("Language with key '" + t_default + "' does not exist in current context. Default language will be 'English'.");
            return "en";
        }
    }
}
