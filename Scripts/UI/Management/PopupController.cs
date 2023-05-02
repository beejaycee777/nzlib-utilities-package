using UnityEngine.Events;

namespace NZLib.Utilities.UI
{
    /// <summary>
    /// Manages PopupController behaviour.
    /// </summary>
    public sealed class PopupController
    {
        #region Singleton

        /// <summary>
        /// Static instance for PopupController.
        /// </summary>
        private static readonly PopupController _instance = new();
        public static PopupController Instance => _instance;

        #endregion

        #region Events

        public UnityAction<string> OnOpenPopup;
        public UnityAction<string> OnClosePopup;
        public UnityAction OnCloseAll;

        #endregion

        /* ===================================================================================== */
        // Base

        private PopupController ()
        {
            CloseAll();
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Opens requested popup.
        /// </summary>
        /// <param name="popup">Popup name to be opened.</param>
        public void Open (string popup)
        {
            OnOpenPopup?.Invoke(popup);
        }

        /// <summary>
        /// Closes requested popup.
        /// </summary>
        /// <param name="popup">Popup name to be closed.</param>
        public void Close (string popup)
        {
            OnClosePopup?.Invoke(popup);
        }

        /// <summary>
        /// Closes any active popup.
        /// </summary>
        public void CloseAll ()
        {
            OnCloseAll?.Invoke();
        }
    }
}
