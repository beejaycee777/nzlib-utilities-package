using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NZLib.Utilities.UI
{
    [RequireComponent(typeof(RectTransform), typeof(Mask))]
    /// <summary>
    /// Manages MaskOnClick behaviour.
    /// </summary>
    public sealed class MaskOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Getters

        /// <summary>
        /// Returns RectTransform component.
        /// </summary>
        private RectTransform _getRect
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
        /// Returns Mask's RectTransform component.
        /// </summary>
        private RectTransform _getMaskRect
        {
            get
            {
                if (_maskRect == null)
                {
                    _maskRect = _mask.GetComponent<RectTransform>();
                }
                return _maskRect;
            }
        }
        /// <summary>
        /// Returns Camera.
        /// </summary>
        private Camera _getCamera
        {
            get
            {
                if (_camera == null)
                {
                    var t_canvas = transform.GetComponentInParent<Canvas>();
                    if (t_canvas.renderMode == RenderMode.ScreenSpaceCamera || t_canvas.renderMode == RenderMode.WorldSpace)
                    {
                        _camera = t_canvas.worldCamera;
                    }
                    else if (t_canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                    {
                        _camera = null;
                    }
                }
                return _camera;
            }
        }

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Total animation duration.
        /// </summary>
        [SerializeField][Range(0.1f, 15f)] private float _duration = 2.5f;
        /// <summary>
        /// Mask color when is inactive.
        /// </summary>
        [SerializeField] private Color _invisibleColor = new(1f, 1f, 1f, 0f);
        /// <summary>
        /// Mask color when is active.
        /// </summary>
        [SerializeField] private Color _visibleColor = new(1f, 1f, 1f, 0.5f);
        /// <summary>
        /// Maximum mask size to reach.
        /// </summary>
        private Vector2 _sizeMax = Vector2.zero;

        /* ===================================================================================== */
        // Components

        /// <summary>
        /// Mask object.
        /// </summary>
        [SerializeField] private Image _mask;
        /// <summary>
        /// RectTransform component.
        /// </summary>
        private RectTransform _rect;
        /// <summary>
        /// Mask's RectTransform component.
        /// </summary>
        private RectTransform _maskRect;
        /// <summary>
        /// Camera used to render.
        /// </summary>
        private Camera _camera;

        /* ===================================================================================== */
        // Base

        private IEnumerator Start ()
        {
            // If object is inside a layout and it's giving it a size,
            // it may return (0, 0) instead of real value. Solution is waiting for the first frame to end. 
            yield return new WaitForEndOfFrame();
            _mask.color = _invisibleColor;
            _getMaskRect.sizeDelta = Vector2.zero;
            _sizeMax = _getRect.sizeDelta * 2.75f;
        }

        public void OnPointerDown (PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_getRect, eventData.pressPosition, _getCamera, out var t_maskPosition);
            _getMaskRect.localPosition = t_maskPosition;
            _sizeMax = VectorHelper.GetCoverAreaSize(_getMaskRect.localPosition, _getRect);
            CoroutineManager.Clear(this);
            var t_coroutines = new IEnumerator[]
            {
                LinearInterpolation.ChangeSize(_getMaskRect, Vector2.zero, _sizeMax, _duration, 0f),
                LinearInterpolation.ChangeColor(_mask, _invisibleColor, _visibleColor, _duration, 0f)
            };
            CoroutineManager.Register(this, t_coroutines);
        }

        public void OnPointerUp (PointerEventData eventData)
        {
            var t_coroutine = LinearInterpolation.ChangeColor(_mask, _visibleColor, _invisibleColor, _duration / 3f, 0.15f);
            CoroutineManager.Register(this, t_coroutine);
        }
    }
}
