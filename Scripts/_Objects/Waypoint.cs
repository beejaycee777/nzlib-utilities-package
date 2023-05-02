using UnityEngine;

namespace NZLib.Utilities.Objects
{
    /// <summary>
	/// Represents a graphic for a Waypoint. It may be useful to display a representation in Editor that will not be shown on execution.
	/// </summary>
    public class Waypoint : MonoBehaviour
    {
        /* ===================================================================================== */
        // Parameters

        [SerializeField] private float _size = 0.1f;
        [SerializeField] private Color _color = Color.yellow;

        /* ===================================================================================== */
        // Base

        private void OnDrawGizmos ()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _size);
        }
    }
}
