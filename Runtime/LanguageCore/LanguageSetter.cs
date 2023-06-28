using UnityEngine;

namespace NZLib.Utilities.LanguageCore
{
    /// <summary>
    /// Gets access to LanguageController from any object in scene.
    /// </summary>
    public sealed class LanguageSetter : MonoBehaviour
    {
        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Sets current Language.
        /// </summary>
        /// <param name="key">Given Language key.</param>
        public void Set (string key) => LanguageController.Instance.SetLanguage(key);
    }
}
