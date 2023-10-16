using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using NZLib.Utilities.DirectionCore;

namespace NZLib.Utilities.UI
{
    [RequireComponent(typeof(Selectable))]
    public class SlideHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Events

        /// <summary>
        /// Called when user performed an slide.
        /// </summary>
        public UnityAction<Direction> OnSlide;
        /// <summary>
        /// Called when user performed an slide up.
        /// </summary>
        public UnityEvent OnSlideUp = new();
        /// <summary>
        /// Called when user performed an slide down.
        /// </summary>
        public UnityEvent OnSlideDown = new();
        /// <summary>
        /// Called when user performed an slide left.
        /// </summary>
        public UnityEvent OnSlideLeft = new();
        /// <summary>
        /// Called when user performed an slide right.
        /// </summary>
        public UnityEvent OnSlideRight = new();

        #endregion

        #region Getters / Setters

        /// <summary>
        /// Returns enabled value.
        /// </summary>
        public bool Enabled
        {
            get => _enabled;
            set { _enabled = value; }
        }
        /// <summary>
        /// Returns horizontal listener value.
        /// </summary>
        public bool ListenHorizontal
        {
            get => _listenHorizontal;
            set { _listenHorizontal = value; }
        }
        /// <summary>
        /// Returns vertical listener value.
        /// </summary>
        public bool ListenVertical
        {
            get => _listenVertical;
            set { _listenVertical = value; }
        }
        /// <summary>
        /// Returns distance value.
        /// </summary>
        public float Distance
        {
            get => _distance;
            set { _distance = value; }
        }

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Enabled flag.
        /// </summary>
        [SerializeField] private bool _enabled = true;
        /// <summary>
        /// When enabled, listens for horizontal events.
        /// </summary>
        [SerializeField] private bool _listenHorizontal = true;
        /// <summary>
        /// When enabled, listens for vertical events.
        /// </summary>
        [SerializeField] private bool _listenVertical = true;
        /// <summary>
        /// Minimum distance to consider slide.
        /// </summary>
        [SerializeField] private float _distance = 100f;

        /* ===================================================================================== */
        // Base

        public void OnBeginDrag (PointerEventData eventData) { }

        public void OnDrag (PointerEventData eventData) { }

        public void OnEndDrag (PointerEventData eventData)
        {
            if (_enabled)
            {
                var t_hDiff = Mathf.Abs(eventData.pressPosition.x - eventData.position.x);
                var t_vDiff = Mathf.Abs(eventData.pressPosition.y - eventData.position.y);

                if (_listenHorizontal == true && _listenVertical == true)
                {
                    if (t_hDiff >= t_vDiff && t_hDiff >= _distance)
                    {
                        if (eventData.pressPosition.x > eventData.position.x)
                        {
                            OnSlide?.Invoke(Direction.Left);
                            OnSlideLeft?.Invoke();
                        }
                        else if (eventData.pressPosition.x < eventData.position.x)
                        {
                            OnSlide?.Invoke(Direction.Right);
                            OnSlideRight?.Invoke();
                        }
                    }
                    else if (t_vDiff >= _distance)
                    {
                        if (eventData.pressPosition.y < eventData.position.y)
                        {
                            OnSlide?.Invoke(Direction.Up);
                            OnSlideUp?.Invoke();
                        }
                        else if (eventData.pressPosition.y > eventData.position.y)
                        {
                            OnSlide?.Invoke(Direction.Down);
                            OnSlideDown?.Invoke();
                        }
                    }
                }
                else if (_listenHorizontal == true && t_hDiff >= _distance)
                {
                    if (eventData.pressPosition.x > eventData.position.x)
                    {
                        OnSlide?.Invoke(Direction.Left);
                        OnSlideLeft?.Invoke();
                    }
                    else if (eventData.pressPosition.x < eventData.position.x)
                    {
                        OnSlide?.Invoke(Direction.Right);
                        OnSlideRight?.Invoke();
                    }
                }
                else if (_listenVertical == true && t_vDiff >= _distance)
                {
                    if (eventData.pressPosition.y < eventData.position.y)
                    {
                        OnSlide?.Invoke(Direction.Up);
                        OnSlideUp?.Invoke();
                    }
                    else if (eventData.pressPosition.y > eventData.position.y)
                    {
                        OnSlide?.Invoke(Direction.Down);
                        OnSlideDown?.Invoke();
                    }
                }
            }
        }
    }
}