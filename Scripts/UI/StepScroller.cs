using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NZLib.Utilities.UI
{
    [RequireComponent(typeof(RectTransform), typeof(RectMask2D), typeof(Image))]
    /// <summary>
    /// Manages StepScroller behaviour.
    /// </summary>
    public abstract class StepScroller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Events

        public UnityAction<int> OnIndexChange;

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Speed when reaching step automatically.
        /// </summary>
        [SerializeField] protected float _speed = 4f;
        /// <summary>
        /// If is more than 0, it will create a wider limit.
        /// </summary>
        [SerializeField] protected float _elasticity = 0f;
        /// <summary>
        /// Current index.
        /// </summary>
        protected int _index = 0;
        /// <summary>
        /// Time during scroll in seconds.
        /// </summary>
        protected float _timeDrag = 0f;
        /// <summary>
        /// Position limits.
        /// </summary>
        protected float[] _limits = new float[2];
        /// <summary>
        /// Position on begin drag.
        /// </summary>
        protected Vector2 _positionOnBegin = Vector2.zero;
        /// <summary>
        /// Contains all steps and positions.
        /// </summary>
        protected Dictionary<int, float> _stepDictionary = new Dictionary<int, float>();

        /* ===================================================================================== */
        // Components

        /// <summary>
        /// Contains all steps.
        /// </summary>
        [SerializeField] protected RectTransform _content;
        /// <summary>
        /// Steps.
        /// </summary>
        [SerializeField] protected RectTransform[] _steps;

        /* ===================================================================================== */
        // Base

        protected IEnumerator Start ()
        {
            yield return new WaitForEndOfFrame();
            // Resets 'Content' position.
            _content.anchoredPosition = Vector2.zero;
            GenerateStepDictionary();
            CreateLimits();
        }

        public abstract void OnBeginDrag (PointerEventData eventData);
        public abstract void OnDrag (PointerEventData eventData);
        public abstract void OnEndDrag (PointerEventData eventData);

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Generates step dictionary. Once in every execution.
        /// </summary>
        protected abstract void GenerateStepDictionary ();

        /// <summary>
        /// Creates minimum and maximum horizontal limits. Once in every execution
        /// </summary>
        protected abstract void CreateLimits ();

        /// <summary>
        /// Sets anchored position while on drag.
        /// </summary>
        protected abstract void SetPosition (Vector2 newPosition);

        /// <summary>
        /// Detects if user has made an slide and executes it.
        /// </summary>
        /// <param name="eventData">EventData related to drag behaviour.</param>
        protected abstract bool Slide (PointerEventData eventData);

        /// <summary>
        /// Reaches nearest step point.
        /// </summary>
        protected abstract void ReachNearestPoint ();

        /// <summary>
        /// Starts coroutine to reach requested step.
        /// </summary>
        /// <param name="step">Requested step to reach.</param>
        protected abstract IEnumerator ReachStep (int step);

        /// <summary>
        /// Sets current index.
        /// </summary>
        /// <param name="index">Current index.</param>
        protected void SetIndex (int index)
        {
            _index = index;
            OnIndexChange?.Invoke(index);
        }

        /// <summary>
        /// Counts dragging time.
        /// </summary>
        protected IEnumerator CountTimeDrag ()
        {
            while (true)
            {
                _timeDrag += Time.deltaTime;
                yield return null;
            }
        }
    }
}
