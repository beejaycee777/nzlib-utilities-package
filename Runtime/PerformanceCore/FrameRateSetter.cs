using UnityEngine;

namespace NZLib.Utilities.PerformanceCore
{
    /// <summary>
    /// Manages FrameRateSetter behaviour.
    /// </summary>
    public sealed class FrameRateSetter : MonoBehaviour
    {
        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Desired framerate.
        /// </summary>
        [SerializeField] private int _frameRate = 60;
        /* ===================================================================================== */
        // Base

        private void Awake () 
        {
            PerformanceHelper.SetFrameRate(_frameRate);
            Destroy(gameObject);
        }
    }
}
