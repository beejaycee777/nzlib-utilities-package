using UnityEngine;

namespace NZLib.Utilities
{
    /// <summary>
    /// Allows generating screen captures by using a key.
    /// </summary>
    public sealed class ScreenCaptureManager : MonoBehaviour
    {
        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// File name for the screenshot.
        /// </summary>
        [SerializeField] private string _fileName = "Screenshot";
        /// <summary>
        /// Screen capture key.
        /// </summary>
        [SerializeField] private KeyCode _captureKey = KeyCode.F5;

        /* ===================================================================================== */
        // Base

        private void Update ()
        {
            if (Input.GetKeyDown(_captureKey))
            {
                Generate();
            }
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Generates a screen capture with the file name requested.
        /// </summary>
        /// <param name="fileName">File name requested.</param>
        public void Generate (string fileName)
        {
            fileName += ".png";
            ScreenCapture.CaptureScreenshot(fileName);
            Debug.Log("Created a new screenshot file with name '" + fileName + "'.");
        }

        /// <summary>
        /// Generates a screen capture.
        /// </summary>
        private void Generate ()
        {
            var t_fileName = _fileName + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            ScreenCapture.CaptureScreenshot(t_fileName);
            Debug.Log("Created a new screenshot file with name '" + t_fileName + "'.");
        }
    }
}