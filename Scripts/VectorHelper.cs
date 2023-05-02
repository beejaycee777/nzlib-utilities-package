using UnityEngine;

namespace NZLib.Utilities
{
    /// <summary>
    /// Manages VectorHelper behaviour.
    /// </summary>
    public sealed class VectorHelper
    {
        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Returns maximum distance from requested point to all RectTransform vertex.
        /// </summary>
        /// <param name="point">Local point inside the rect.</param>
        /// <param name="content">RectTransform that contains the point.</param>
        /// <returns>Maximum distance from requested point to all RectTransform vertex.</returns>
        public static float GetMaximumDistanceToVertex (Vector2 point, RectTransform content)
        {
            var t_vertex = new Vector2[4]
            {
                new Vector2(- content.rect.size.x / 2f, content.rect.size.y / 2f),
                new Vector2(- content.rect.size.x / 2f, - content.rect.size.y / 2f),
                new Vector2(content.rect.size.x / 2f, content.rect.size.y / 2f),
                new Vector2(content.rect.size.x / 2f, - content.rect.size.y / 2f),
            };

            var t_maxDistance = 0f;
            for (var i = 0; i < t_vertex.Length; i++)
            {
                var t_distance = Vector2.Distance(point, t_vertex[i]);
                if (t_distance > t_maxDistance)
                {
                    t_maxDistance = t_distance;
                }
            }
            return t_maxDistance;
        }

        /// <summary>
        /// Returns necessary size for an object from given point to cover all content area.
        /// </summary>
        /// <param name="point">Local point inside the rect.</param>
        /// <param name="content">RectTransform that contains the point.</param>
        /// <returns>Necessary size for an object from given point to cover all content area.</returns>
        public static Vector2 GetCoverAreaSize (Vector2 point, RectTransform content)
        {
            var t_maxDistance = GetMaximumDistanceToVertex (point, content);
            var t_diameter = t_maxDistance * 2f;
            return new Vector2(t_diameter, t_diameter);
        }
    }
}
