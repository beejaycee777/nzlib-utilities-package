using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace NZLib.Utilities.UI
{
    /// <summary>
    /// Manages Popup behaviour.
    /// </summary>
    public abstract class Popup : MonoBehaviour
    {
        #region Events

        public UnityEvent OnOpen = new();
        public UnityEvent OnClose = new();

        #endregion

        #region Getters

        /// <summary>
        /// Returns RectTransform component.
        /// </summary>
        protected RectTransform _getRect
        {
            get
            {
                if (_rect == null)
                {
                    _rect = GetComponent<RectTransform>();
                }
                return _rect;
            }
        }
        /// <summary>
        /// Returns Image.
        /// </summary>
        protected Image _getImage
        {
            get
            {
                if (_image == null)
                {
                    _image = GetComponent<Image>();
                }
                return _image;
            }
        }
        /// <summary>
        /// Returns CanvasGroup component.
        /// </summary>
        protected CanvasGroup _getCanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = GetComponentInChildren<CanvasGroup>();
                }
                return _canvasGroup;
            }
        }

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Flag that forces popup to close whenever any other popup has been opened.
        /// </summary>
        [SerializeField] protected bool _forceClose = false;
        /// <summary>
        /// Flag that is enabled when popup is opened.
        /// </summary>
        protected bool _active = false;
        /// <summary>
        /// Normal size by default.
        /// </summary>
        protected Vector2 _sizeDefault = Vector2.zero;

        /* ===================================================================================== */
        // Components

        /// <summary>
        /// RectTransform component.
        /// </summary>
        protected RectTransform _rect;
        /// <summary>
        /// Image component.
        /// </summary>
        protected Image _image;
        /// <summary>
        /// Content's CanvasGroup component.
        /// </summary>
        protected CanvasGroup _canvasGroup;

        /* ===================================================================================== */
        // Base

        protected void Start () 
        {
            PopupController.Instance.OnOpenPopup += HandleOnOpenPopup;
            PopupController.Instance.OnClosePopup += HandleOnClosePopup;
            PopupController.Instance.OnCloseAll += HandleOnCloseAll;
            _getCanvasGroup.alpha = 0;
            _getCanvasGroup.blocksRaycasts = false;
            _sizeDefault = _getRect.sizeDelta;
        }

        protected void OnDestroy ()
        {
            PopupController.Instance.OnOpenPopup -= HandleOnOpenPopup;
            PopupController.Instance.OnClosePopup -= HandleOnClosePopup;
            PopupController.Instance.OnCloseAll -= HandleOnCloseAll;
        }

        protected void OnApplicationQuit ()
        {
            PopupController.Instance.OnOpenPopup -= HandleOnOpenPopup;
            PopupController.Instance.OnClosePopup -= HandleOnClosePopup;
            PopupController.Instance.OnCloseAll -= HandleOnCloseAll;
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Forces open status from any object in scene.
        /// </summary>
        public void ForceOpen ()
        {
            PopupController.Instance.Open(name);
        }

        /// <summary>
        /// Forces close status from any object in scene.
        /// </summary>
        public void ForceClose ()
        {
            PopupController.Instance.Close(name);
        }

        /// <summary>
        /// Displays on screen.
        /// </summary>
        protected virtual void Open ()
        {
            if (_active == false)
            {
                _active = true;
                OnOpen?.Invoke();
            }            
        }

        /// <summary>
        /// Hides from screen.
        /// </summary>
        protected virtual void Close ()
        {
            if (_active == true)
            {
                _active = false;
                OnClose?.Invoke();
            }           
        }

        /// <summary>
        /// Executes events when a Popup has been opened.
        /// </summary>
        /// <param name="popup">Opened Popup name.</param>
        protected void HandleOnOpenPopup (string popup)
        {
            if (popup == name)
            {
                Open();
            }
            else if (_forceClose == true)
            {
                Close();
            }
        }

        /// <summary>
        /// Executes events when a Popup has been closed.
        /// </summary>
        /// <param name="popup">Closed Popup name</param>
        protected void HandleOnClosePopup (string popup)
        {
            if (popup == name)
            {
                Close();
            }
        }

        /// <summary>
        /// Executes events when all Popups have to be closed.
        /// </summary>
        protected void HandleOnCloseAll ()
        {
            Close();
        }
    }
}
