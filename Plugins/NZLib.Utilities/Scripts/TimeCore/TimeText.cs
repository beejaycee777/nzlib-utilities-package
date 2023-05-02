using UnityEngine;

namespace NZLib.Utilities.TimeCore
{
    /// <summary>
    /// Displays real time on a Text component.
    /// </summary>
    public abstract class TimeText : MonoBehaviour
    {
        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Delay time in seconds.
        /// </summary>
        [SerializeField] protected float _delay = 5f;
        /// <summary>
        /// Time format.
        /// </summary>
        [SerializeField] protected string _format = "HH:mm:ss";

        /* ===================================================================================== */
        // Base

        protected void OnEnable () => InvokeRepeating(nameof(Display), 0, _delay);
        protected void OnDisable () => CancelInvoke();

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Updates to current system time.
        /// </summary>
        protected abstract void Display ();
    }
}
