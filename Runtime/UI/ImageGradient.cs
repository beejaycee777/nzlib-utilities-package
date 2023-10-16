using UnityEngine;
using UnityEngine.UI;

namespace NZLib.Utilities.UI
{
    [AddComponentMenu("UI/Effects/Gradient")]
    public class ImageGradient : BaseMeshEffect
    {
        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Starting color.
        /// </summary>
        [SerializeField] private Color _colorStart = Color.white;
        /// <summary>
        /// Ending color.
        /// </summary>
        [SerializeField] private Color _colorEnd = Color.white;
        /// <summary>
        /// Angle.
        /// </summary>
        [SerializeField] [Range(-180f, 180f)] private float _angle = 0f;
        /// <summary>
        /// Flag to ignore ratio.
        /// </summary>
        [SerializeField] private bool _ignoreRatio = true;

        /* ===================================================================================== */
        // methods

        public override void ModifyMesh (VertexHelper vertexHelper)
        {
            if (enabled)
            {
                var t_rect = graphic.rectTransform.rect;
                var t_direction = ImageGradientUtils.RotationDir(_angle);
                if (!_ignoreRatio)
                {
                    t_direction = ImageGradientUtils.CompensateAspectRatio(t_rect, t_direction);
                }

                var localPositionMatrix = ImageGradientUtils.LocalPositionMatrix(t_rect, t_direction);
                var vertex = default(UIVertex);
                for (int i = 0; i < vertexHelper.currentVertCount; i++)
                {
                    vertexHelper.PopulateUIVertex(ref vertex, i);
                    Vector2 localPosition = localPositionMatrix * vertex.position;
                    vertex.color *= Color.Lerp(_colorEnd, _colorStart, localPosition.y);
                    vertexHelper.SetUIVertex(vertex, i);
                }
            }
        }
    }
}