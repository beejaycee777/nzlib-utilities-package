using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NZLib.Utilities.UI;

namespace NZLib.Utilities
{
	[RequireComponent(typeof(Image), typeof(Canvas), typeof(GraphicRaycaster))]
    /// <summary>
    /// Manages Blocker behaviour.
    /// </summary>
    public class Blocker : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        protected class Properties
        {
            public readonly int Layer = 29999;
            public readonly float Duration = 1f;
            public readonly Color ColorInactive = new(0f, 0f, 0f, 0f);
            public readonly Color ColorActive = new(0f, 0f, 0f, 0.75f);

            public Properties () { }

            public Properties (int layer, float duration, Color colorInactive, Color colorActive)
            {
                Layer = layer;
                Duration = duration;
                ColorInactive = colorInactive;
                ColorActive = colorActive;
            }
        }

        #region Getters

        /// <summary>
        /// Returns Image component.
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

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// When TRUE, it activates some handlers related to Popup management.
        /// </summary>
        [SerializeField] private bool _linkToPopups = true;

        /* ===================================================================================== */
        // Components

        /// <summary>
        /// Contains all properties.
        /// </summary>
        protected Properties _properties = new();
        /// <summary>
        /// Image component.
        /// </summary>
        protected Image _image = null;

        /* ===================================================================================== */
        // Base

        protected void Start () 
        {
            _getImage.color = Color.clear;
            _getImage.raycastTarget = false;
            GetComponent<Canvas>().sortingOrder = _properties.Layer;

            if (_linkToPopups)
            {
                PopupController.Instance.OnOpenPopup += HandleOnOpenPopup;
                PopupController.Instance.OnClosePopup += HandleOnClosePopup;
                PopupController.Instance.OnCloseAll += HandleOnCloseAllPopups;
            }            
        }

        protected void OnDestroy ()
        {
            if (_linkToPopups)
            {
                PopupController.Instance.OnOpenPopup -= HandleOnOpenPopup;
                PopupController.Instance.OnClosePopup -= HandleOnClosePopup;
                PopupController.Instance.OnCloseAll -= HandleOnCloseAllPopups;
            }
        }

        protected void OnApplicationQuit ()
        {
            if (_linkToPopups)
            {
                PopupController.Instance.OnOpenPopup -= HandleOnOpenPopup;
                PopupController.Instance.OnClosePopup -= HandleOnClosePopup;
                PopupController.Instance.OnCloseAll -= HandleOnCloseAllPopups;
            }
        }

        public void OnPointerDown (PointerEventData eventData)
        {
            //
        }

        public void OnPointerUp (PointerEventData eventData)
        {
            HandleOnClick();
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Displays blocker.
        /// </summary>
        public void Display ()
        {
            CoroutineManager.Clear(this);
            var t_coroutine = LinearInterpolation.ChangeColor(_getImage, _getImage.color, _properties.ColorActive, _properties.Duration, 0f);
            CoroutineManager.Register(this, t_coroutine);
            _getImage.raycastTarget = true;
        }

        /// <summary>
        /// Hides blocker.
        /// </summary>
        public void Hide ()
        {
            CoroutineManager.Clear(this);
            var t_coroutine = LinearInterpolation.ChangeColor(_getImage, _getImage.color, _properties.ColorInactive, _properties.Duration, 0f);
            CoroutineManager.Register(this, t_coroutine);
            _getImage.raycastTarget = false;
        }

        /// <summary>
        /// Changes properties.
        /// </summary>
        /// <param name="duration">Fade duration.</param>
        /// <param name="colorActive">Color while active.</param>
        /// <param name="colorInactive">Color while inactive.</param>
        public void ChangeProperties (float duration, int layer, Color colorActive, Color colorInactive)
        {
            _properties = new(layer, duration, colorActive, colorInactive);
            GetComponent<Canvas>().sortingOrder = layer;
        }

        /// <summary>
        /// Executes events when user clicks on it.
        /// </summary>
        protected void HandleOnClick ()
        {
            if (_linkToPopups)
            {
                PopupController.Instance.CloseAll();
            }
        }

        /// <summary>
        /// Executes events when a Popup has been opened.
        /// </summary>
        /// <param name="popup">Opened Popup name.</param>
        protected void HandleOnOpenPopup (string popup)
        {
            Display();
        }

        /// <summary>
        /// Executes events when a Popup has been closed.
        /// </summary>
        /// <param name="popup">Closed Popup name.</param>
        protected void HandleOnClosePopup (string popup)
        {
            Hide();
        }

        /// <summary>
        /// Executes events when all Popups must close.
        /// </summary>
        protected void HandleOnCloseAllPopups ()
        {
            Hide();
        }
    }
}
