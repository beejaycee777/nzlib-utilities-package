using UnityEngine;

namespace NZLib.Utilities.InputCore
{
    /// <summary>
    /// Manages Mouse behaviour.
    /// </summary>
    public sealed class Mouse : MonoBehaviour
    {
        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Current visibility status.
        /// </summary>
        [SerializeField] private bool _display = true;
        /// <summary>
        /// Key used to toggle display status.
        /// </summary>
        [SerializeField] private KeyCode _toggleDisplay = KeyCode.None;

        /* ===================================================================================== */
        // Base

        private void Start () 
        {
            Cursor.visible = _display;
        }
        
        private void Update ()
        {
            if (Input.GetKeyDown(_toggleDisplay))
            {
                ToggleDisplay();
            }
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Handles events on input 'Toggle display' key.
        /// </summary>
        public void ToggleDisplay ()
        {
            _display = !_display;
            Cursor.visible = _display;
        }
    }
}
