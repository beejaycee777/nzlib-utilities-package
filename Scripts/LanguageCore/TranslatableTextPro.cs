using UnityEngine;
using TMPro;

namespace NZLib.Utilities.LanguageCore
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TranslatableTextPro : TranslatableText
    {
        #region Getters

        private TextMeshProUGUI _getText
        {
            get
            {
                if (_text == null)
                {
                    _text = GetComponent<TextMeshProUGUI>();
                }
                return _text;
            }
        }

        #endregion

        /* ===================================================================================== */
        // Parameters

        private TextMeshProUGUI _text;

        /* ===================================================================================== */
        // Methods

        public override void DisplayValue ()
        {
            if (_key.Length > 0)
            {
                _getText.text = GetValue;
            }
        }
    }
}
