using System;
using UnityEngine;
using UnityEngine.UI;

namespace NZLib.Utilities.TimeCore
{
    [RequireComponent(typeof(Text))]
    public sealed class TimeTextLegacy : TimeText
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
            var t_time = DateTime.Now;
            _getText.text = t_time.ToString(_format);
        }
    }
}
