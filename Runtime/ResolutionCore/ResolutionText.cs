using UnityEngine;

namespace NZLib.Utilities.ResolutionCore
{
    /// <summary>
    /// Displays screen resolution on a Text component.
    /// </summary>
    public abstract class ResolutionText : MonoBehaviour
    {
        /* ===================================================================================== */
        // Methods

        protected void OnEnable () => Display();

        /* ===================================================================================== */
        // Methods
        
        /// <summary>
        /// Displays resolution on Text.
        /// </summary>
        protected abstract void Display ();
    }
}
