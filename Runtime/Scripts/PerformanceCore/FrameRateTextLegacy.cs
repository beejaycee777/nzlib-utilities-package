using UnityEngine;
using UnityEngine.UI;

namespace NZLib.Utilities.PerformanceCore
{
    public class FrameRateTextLegacy : FrameRateText
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
