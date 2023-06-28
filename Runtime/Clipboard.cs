using UnityEngine;
using UnityEngine.Events;

namespace NZLib.Utilities
{
    /// <summary>
    /// Manages Clipboard operations.
    /// </summary>
    public static class Clipboard
    {
        /// <summary>
        /// Events triggered every time a text is copied to the clipboard.
        /// </summary>
        public static UnityAction<string> OnCopyToClipboard;

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Copies requested text to the clipboard.
        /// </summary>
        /// <param name="text">Text to copy.</param>
        public static void CopyToClipboard (string text)
        {
            GUIUtility.systemCopyBuffer = text;
            OnCopyToClipboard?.Invoke(text);
        }
    }
}
