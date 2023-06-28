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

        #region Getters

        /// <summary>
        /// Returns active Popup.
        /// </summary>
        public string GetActivePopup => _activePopup;

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Active Popup's name.
        /// </summary>
        private string _activePopup = "";
        /// <summary>
        /// Returns current active status.
        /// </summary>
        private bool _isActive = false;

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
            _isActive = true;
            _activePopup = popup;
            OnOpenPopup?.Invoke(popup);
        }

        /// <summary>
        /// Closes requested popup.
        /// </summary>
        /// <param name="popup">Popup name to be closed.</param>
        public void Close (string popup)
        {
            _isActive = false;
            _activePopup = "";
            OnClosePopup?.Invoke(popup);
        }

        /// <summary>
        /// Closes any active popup.
        /// </summary>
        public void CloseAll ()
        {
            _isActive = false;
            _activePopup = "";
            OnCloseAll?.Invoke();
        }

        /// <summary>
        /// Returns TRUE if there is any Popup active.
        /// </summary>
        /// <returns>TRUE if there is any Popup active.</returns>
        public bool IsAnyPopupActive ()
        {
            return _isActive;
        }
    }
}
