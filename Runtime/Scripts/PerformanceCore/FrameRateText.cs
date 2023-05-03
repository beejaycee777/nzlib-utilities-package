using UnityEngine;

namespace NZLib.Utilities.PerformanceCore
{
    /// <summary>
    /// Displays frame rate per second on a Text component.
    /// </summary>
    public abstract class FrameRateText : MonoBehaviour
    {
        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Delay time in seconds.
        /// </summary>
        [SerializeField] protected float _delay = 1f;
        /// <summary>
        /// Displays 'fps' with the value.
        /// </summary>
        [SerializeField] protected bool _displayLabel = false;

        /* ===================================================================================== */
        // Base

        protected void OnEnable () => InvokeRepeating(nameof(Display), 0, _delay);
        protected void OnDisable () => CancelInvoke();

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Updates to current frame rate per second.
        /// </summary>
        protected abstract void Display ();
    }
}
