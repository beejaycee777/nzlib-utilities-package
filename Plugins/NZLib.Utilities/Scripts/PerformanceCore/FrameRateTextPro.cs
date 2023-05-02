using UnityEngine;
using TMPro;

namespace NZLib.Utilities.PerformanceCore
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FrameRateTextPro : FrameRateText
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

        protected override void Display ()
        {
            _getText.text = PerformanceHelper.GetFrameRate().ToString();
            if (_displayLabel)
            {
                _getText.text += "fps";
            }
        }
    }
}
