using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NZLib.Utilities
{
    /// <summary>
    /// Generates a linear interpolation automatically.
    /// </summary>
    public class LinearInterpolation
    {
        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Returns result of a linear interpolation between given values.
        /// </summary>
        /// <param name="t">X value that will be converted to a Y value.</param>
        /// <param name="xMin">X range minimum value.</param>
        /// <param name="xMax">X range maximum value.</param>
        /// <param name="yMin">Y range minimum value.</param>
        /// <param name="yMax">Y range maximum value.</param>
        /// <returns>Result of a linear interpolation.</returns>
        public static float GetResult (float t, float xMin, float xMax, float yMin, float yMax)
        {
            return ((t - xMin) / (xMax - xMin) * (yMax - yMin)) + yMin;
        }

        /// <summary>
        /// Returns a new coroutine that lerps position with requested data.
        /// </summary>
        /// <param name="tr">Transform component.</param>
        /// <param name="start">Starting position.</param>
        /// <param name="target">Target position.</param>
        /// <param name="duration">Lerp duration.</param>
        /// <param name="delay">Delay before executing.</param>
        public static IEnumerator ChangePosition (Transform tr, Vector2 start, Vector2 target, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            var t_timeElapsed = 0f;
            tr.localPosition = start;
            while (t_timeElapsed < duration && Vector2.Distance(tr.localPosition, target) > 0.01f)
            {
                tr.localPosition = Vector2.Lerp(tr.localPosition, target, t_timeElapsed / duration);
                t_timeElapsed += Time.deltaTime;
                yield return null;
            }
            tr.localPosition = target;
        }

        /// <summary>
        /// Returns a new coroutine that lerps position with requested data.
        /// </summary>
        /// <param name="rect">RectTransform component.</param>
        /// <param name="start">Starting position.</param>
        /// <param name="target">Target position.</param>
        /// <param name="duration">Lerp duration.</param>
        /// <param name="delay">Delay before executing.</param>
        public static IEnumerator ChangePosition (RectTransform rect, Vector2 start, Vector2 target, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            var t_timeElapsed = 0f;
            rect.anchoredPosition = start;
            while (t_timeElapsed < duration && Vector2.Distance(rect.anchoredPosition, target) > 0.01f)
            {
                rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, target, t_timeElapsed / duration);
                t_timeElapsed += Time.deltaTime;
                yield return null;
            }
            rect.anchoredPosition = target;
        }

        /// <summary>
        /// Returns a new coroutine that lerps size with requested data.
        /// </summary>
        /// <param name="rect">RectTransform component.</param>
        /// <param name="start">Starting size.</param>
        /// <param name="target">Target size.</param>
        /// <param name="duration">Lerp duration.</param>
        /// <param name="delay">Delay before executing.</param>
        public static IEnumerator ChangeSize (RectTransform rect, Vector2 start, Vector2 target, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            var t_timeElapsed = 0f;
            rect.sizeDelta = start;
            while (t_timeElapsed < duration && Vector2.Distance(rect.sizeDelta, target) > 0.01f)
            {
                rect.sizeDelta = Vector2.Lerp(rect.sizeDelta, target, t_timeElapsed / duration);
                t_timeElapsed += Time.deltaTime;
                yield return null;
            }
            rect.sizeDelta = target;
        }


        /// <summary>
        /// Returns a new coroutine that lerps local scale with requested data.
        /// </summary>
        /// <param name="tr">Transform component to scale.</param>
        /// <param name="start">Starting size.</param>
        /// <param name="target">Target size.</param>
        /// <param name="duration">Lerp duration.</param>
        /// <param name="delay">Delay before executing.</param>
        public static IEnumerator ChangeScale (Transform tr, Vector2 start, Vector2 target, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            var t_timeElapsed = 0f;
            tr.localScale = start;
            while (t_timeElapsed < duration && Vector2.Distance(tr.localScale, target) > 0.01f)
            {
                tr.localScale = Vector2.Lerp(tr.localScale, target, t_timeElapsed / duration);
                t_timeElapsed += Time.deltaTime;
                yield return null;
            }
            tr.localScale = target;
        }

        /// <summary>
        /// Returns a new coroutine that lerps color with requested data.
        /// </summary>
        /// <param name="img">Image component.</param>
        /// <param name="start">Starting color.</param>
        /// <param name="target">Target color.</param>
        /// <param name="duration">Lerp duration.</param>
        /// <param name="delay">Delay before executing.</param>
        public static IEnumerator ChangeColor (Image img, Color start, Color target, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            var t_timeElapsed = 0f;
            img.color = start;
            while (t_timeElapsed < duration && Vector4.Distance(img.color, target) > 0.01f)
            {
                img.color = Color.Lerp(img.color, target, t_timeElapsed / duration);
                t_timeElapsed += Time.deltaTime;
                yield return null;
            }
            img.color = target;
        }

        /// <summary>
        /// Returns a new coroutine that lerps CanvasGroup properties with requested data.
        /// </summary>
        /// <param name="group">CanvasGroup component.</param>
        /// <param name="alphaStart">Starting alpha value.</param>
        /// <param name="alphaTarget">Target alpha value.</param>
        /// <param name="duration">Lerp duration.</param>
        /// <param name="delay">Delay before executing.</param>
        public static IEnumerator ChangeCanvasGroup (CanvasGroup group, float alphaStart, float alphaTarget, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            var t_timeElapsed = 0f;
            group.blocksRaycasts = false;
            group.alpha = alphaStart;
            while (t_timeElapsed < duration && Mathf.Abs(group.alpha - alphaTarget) > 0.01f)
            {
                group.alpha = Mathf.Lerp(group.alpha, alphaTarget, t_timeElapsed / duration);
                t_timeElapsed += Time.deltaTime;
                yield return null;
            }
            group.alpha = alphaTarget;
            group.blocksRaycasts = alphaTarget == 1f;
        }
    }
}
