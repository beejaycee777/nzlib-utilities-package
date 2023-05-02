using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NZLib.Utilities.UI
{
    [RequireComponent(typeof(RectTransform))]
    /// <summary>
    /// Manages ScaleOnClick behaviour.
    /// </summary>
    public sealed class ScaleOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Maximum scale to reach.
        /// </summary>
        [SerializeField] private Vector2 _scaleMax = new(1.25f, 1.25f);
        /// <summary>
        /// Total animation duration.
        /// </summary>
        [SerializeField][Range(1f, 15f)] private float _duration = 1f;

        /* ===================================================================================== */
        // Base

        private void Start ()
        {
            transform.localScale = Vector2.one;
        }

        public void OnPointerDown (PointerEventData eventData)
        {
            CoroutineManager.Clear(this);
            var t_coroutine = LinearInterpolation.ChangeScale(transform, transform.localScale, _scaleMax, _duration, 0f);
            CoroutineManager.Register(this, t_coroutine);
        }

        public void OnPointerUp (PointerEventData eventData)
        {
            CoroutineManager.Clear(this);
            var t_coroutine = LinearInterpolation.ChangeScale(transform, transform.localScale, Vector2.one, _duration, 0f);
            CoroutineManager.Register(this, t_coroutine);
        }
    }
}
