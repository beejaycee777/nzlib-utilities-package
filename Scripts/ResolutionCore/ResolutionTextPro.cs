using UnityEngine;
using TMPro;

namespace NZLib.Utilities.ResolutionCore
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ResolutionTextPro : ResolutionText
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
            var t_resolution = ResolutionHelper.GetCurrent().width.ToString() + "x" + ResolutionHelper.GetCurrent().height.ToString();
            _getText.text = t_resolution;
        }
    }
}
