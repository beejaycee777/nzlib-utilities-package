using UnityEngine;

namespace NZLib.Utilities.LanguageCore
{
    public abstract class TranslatableText : MonoBehaviour
    {
        #region Getters

        public string GetValue => LanguageController.Instance.GetDictionaryValue(_key);

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Keyword which will connect to a word or expression in a Language dictionary.
        /// </summary>
        [SerializeField] protected string _key = "";

        /* ===================================================================================== */
        // Base

        protected void OnDestroy ()
        {
            LanguageController.Instance.OnLanguageChange -= DisplayValue;
        }

        protected void OnEnable ()
        {
            LanguageController.Instance.OnLanguageChange += DisplayValue;
            DisplayValue();
        }

        protected void OnDisable ()
        {
            LanguageController.Instance.OnLanguageChange -= DisplayValue;
        }

        protected void OnApplicationQuit ()
        {
            LanguageController.Instance.OnLanguageChange -= DisplayValue;
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Sets a new Key.
        /// </summary>
        public void SetKey (string newKey)
        {
            _key = newKey;
            DisplayValue();
        }

        /// <summary>
        /// Displays word or expression related to the key in the Language dictionary.
        /// </summary>
        public abstract void DisplayValue ();
    }
}
