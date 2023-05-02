using System;
using UnityEngine;
using TMPro;

namespace NZLib.Utilities.TimeCore
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TimeTextPro : TimeText
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
            var t_time = DateTime.Now;
            _getText.text = t_time.ToString(_format);
        }
    }
}