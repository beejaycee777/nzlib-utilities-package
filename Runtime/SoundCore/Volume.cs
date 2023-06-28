using UnityEngine;
using UnityEngine.Events;

namespace NZLib.Utilities.SoundCore
{
    /// <summary>
    /// Manages Volume value.
    /// </summary>
    public class Volume
    {
        #region Events

        public UnityAction<float> OnValueChange;

        #endregion

        #region Getters / Setters

        public float Value
        {
            get => _value;
            set
            {
                var t_newValue = Mathf.Clamp(value, 0f, 1f);
                if (_value != t_newValue)
                {
                    _value = t_newValue;
                    OnValueChange?.Invoke(_value);
                }
            }
        }

        #endregion

        /* ===================================================================================== */
        // Parameters

        private readonly float _unit = 0.1f;
        private float _value = 1f;

        /* ===================================================================================== */
        // Base

        public Volume () { }

        /// <param name="val">Starting value.</param>
        public Volume (float val)
        {
            _value = val;
        }

        /// <param name="val">Starting value</param>
        /// <param name="unit">Unit that will be used to increase of decrease value one by one.</param>
        public Volume (float val, float unit)
        {
            _value = val;
            _unit = unit;
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Increases value by one unit.
        /// </summary>
        public void Increase () => Value += _unit;

        /// <summary>
        /// Decreases value by one unit.
        /// </summary>
        public void Decrease () => Value -= _unit;
    }
}
