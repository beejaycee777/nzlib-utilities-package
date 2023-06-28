using UnityEngine;

namespace NZLib.Utilities.PerformanceCore
{
    public class PerformanceHelper
    {
        /// <summary>
        /// Sets frame rate.
        /// </summary>
        /// <param name="frameRate">Desired frame rate.</param>
        public static void SetFrameRate (int frameRate)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = frameRate;
            Debug.Log("Framerate set to '" + frameRate + "'.");
        }

        /// <summary>
        /// Returns current frame rate.
        /// </summary>
        /// <returns>Current rate frame.</returns>
        public static int GetFrameRate ()
        {
            return (int)(1f / Time.unscaledDeltaTime);
        }
    }
}
