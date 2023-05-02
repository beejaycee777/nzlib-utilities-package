using UnityEngine;
using UnityEngine.UI;

namespace NZLib.Utilities.LanguageCore
{
    [RequireComponent(typeof(Text))]
    public sealed class TranslatableTextLegacy : TranslatableText
    {
        #region Getters

        private Text _getText
        {
            get
            {
                if (_text == null)
                {
                    _text = GetComponent<Text>();
                }
                return _text;
            }
        }

        #endregion

        /* ===================================================================================== */
        // Parameters

        private Text _text;

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
