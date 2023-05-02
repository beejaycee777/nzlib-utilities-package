using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NZLib.Utilities.UI
{
    [RequireComponent(typeof(RectTransform), typeof(RectMask2D), typeof(Image))]
    /// <summary>
    /// Manages StepScrollerHorizontal behaviour.
    /// # Instructions:
    /// - Add this to any object.
    /// - Create a child named 'Content' and add its reference manually from the inspector.
    /// - Put all RectTransform steps into 'Content' and add their references manually from the inspector.
    /// - Ideally, 'Content' will have an 'HorizontalLayoutGroup' component to manage steps inside and
    ///   a 'ContentSizeFitter' with 'HorizontalFit' on 'PreferredSize'.
    /// - 'Content' pivot must be (0, 1).
    /// - All steps must have the same size and their pivots must be (0, 1).
    /// - 'StepScroller' width must be equal to steps' width.
    /// </summary>
    public sealed class StepScrollerHorizontal : StepScroller
    {
        /* ===================================================================================== */
        // Base

        public override void OnBeginDrag (PointerEventData eventData)
        {
            StopAllCoroutines();
            _timeDrag = 0f;
            StartCoroutine(CountTimeDrag());
            _positionOnBegin = _content.anchoredPosition;
        }

        public override void OnDrag (PointerEventData eventData)
        {
            SetPosition(_positionOnBegin + (eventData.position - eventData.pressPosition));
        }

        public override void OnEndDrag (PointerEventData eventData)
        {
            StopAllCoroutines();
            if (Slide(eventData))
            {
                return;
            }
            ReachNearestPoint();
        }

        /* ===================================================================================== */
        // Methods

        protected override void GenerateStepDictionary ()
        {
            for (var i = 0; i < _steps.Length; i++)
            {
                _steps[i].pivot = new Vector2(0f, 1f);
                var t_position = _steps[i].anchoredPosition.x;
                _stepDictionary.Add(i, _content.anchoredPosition.x - t_position);
            }
        }

        protected override void CreateLimits ()
        {
            _limits = new float[2]
            {
                GetComponent<RectTransform>().sizeDelta.x - _content.sizeDelta.x - _elasticity,
                0f + _elasticity
            };
        }

        protected override void SetPosition (Vector2 newPosition)
        {
            var t_pos = _positionOnBegin;
            t_pos.x = Mathf.Clamp(newPosition.x, _limits[0], _limits[1]);
            if (_content.anchoredPosition != t_pos)
            {
                _content.anchoredPosition = t_pos;
            }
        }

        protected override bool Slide (PointerEventData eventData)
        {
            if (_timeDrag < 0.25f && Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > 75f)
            {
                if (eventData.position.x > eventData.pressPosition.x)
                {
                    StartCoroutine(ReachStep(_index - 1));
                }
                else
                {
                    StartCoroutine(ReachStep(_index + 1));
                }
                return true;
            }
            return false;
        }

        protected override void ReachNearestPoint ()
        {
            var t_stepIndex = 0;
            var t_minDistance = Mathf.Abs(_content.anchoredPosition.x - _stepDictionary[0]);
            for (var i = 1; i < _stepDictionary.Count; ++i)
            {
                var t_distance = Mathf.Abs(_content.anchoredPosition.x - _stepDictionary[i]);
                if (t_distance < t_minDistance)
                {
                    t_minDistance = t_distance;
                    t_stepIndex = i;
                }
            }
            StartCoroutine(ReachStep(t_stepIndex));
        }

        protected override IEnumerator ReachStep (int step)
        {
            step = Mathf.Clamp(step, 0, _steps.Length - 1);
            var t_timeElapsed = 0f;
            while (t_timeElapsed < _speed && Mathf.Abs(_content.anchoredPosition.x - _stepDictionary[step]) > 0.5f)
            {
                var t_position = _content.anchoredPosition;
                t_position.x = Mathf.Lerp(t_position.x, _stepDictionary[step], t_timeElapsed / _speed);
                _content.anchoredPosition = t_position;
                t_timeElapsed += Time.deltaTime;
                yield return null;
            }
            _content.anchoredPosition = new Vector2(_stepDictionary[step], _content.anchoredPosition.y);
            SetIndex(step);
        }
    }
}
