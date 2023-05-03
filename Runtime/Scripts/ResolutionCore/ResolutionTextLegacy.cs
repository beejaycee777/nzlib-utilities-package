using UnityEngine;
using UnityEngine.UI;

namespace NZLib.Utilities.ResolutionCore
{
    [RequireComponent(typeof(Text))]
    public class ResolutionTextLegacy : ResolutionText
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
            var t_resolution = ResolutionHelper.GetCurrent().width.ToString() + "x" + ResolutionHelper.GetCurrent().height.ToString();
            _getText.text = t_resolution;
        }        
    }
}
