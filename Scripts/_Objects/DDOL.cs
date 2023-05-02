using UnityEngine;

namespace NZLib.Utilities
{
    /// <summary>
    /// Makes object indestructible among scenes.
    /// </summary>
    public sealed class DDOL : MonoBehaviour
    {
        /* ===================================================================================== */
        // Base 

        private void Start ()
        {
            DontDestroyOnLoad(gameObject);
            Destroy(this);
        }
    }
}