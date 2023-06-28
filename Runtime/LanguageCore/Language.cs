using System.Collections.Generic;
using UnityEngine;

namespace NZLib.Utilities.LanguageCore
{
    /// <summary>
    /// Contents all necessary information about a single Language.
    /// </summary>
    public class Language
    {
        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Keyword which is useful to identify this Language. Example: "English" -> "en".
        /// </summary>
        public readonly string Key;
        /// <summary>
        /// Universal name of this Languange.
        /// </summary>
        public readonly string Label;
        /// <summary>
        /// Language in system.
        /// </summary>
        public readonly SystemLanguage SystemLanguage;
        /// <summary>
        /// Dictionary that contains every word and expression associated to a unique key.
        /// </summary>
        public readonly Dictionary<string, string> Dictionary;

        /* ===================================================================================== */
        // Base

        public Language (string newKey, string newLabel, Dictionary<string, string> newDictionary)
        {
            Key = newKey;
            Label = newLabel;
            Dictionary = newDictionary;

            switch (Key)
            {
                case "en":
                {
                    SystemLanguage = SystemLanguage.English;
                }
                break;
                case "es":
                {
                    SystemLanguage = SystemLanguage.Spanish;
                }
                break;
            }
        }
    }
}
