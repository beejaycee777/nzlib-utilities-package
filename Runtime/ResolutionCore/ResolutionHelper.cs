using UnityEngine;

namespace NZLib.Utilities.ResolutionCore
{
    public class ResolutionHelper
    {
        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Finds out current screen resolution and returns it.
        /// </summary>
        public static Resolution GetCurrent ()
        {
            var t_resolution = Screen.currentResolution;
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
                System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                t_resolution = (Resolution)GetSizeOfMainGameView.Invoke(null, null); //(Vector2)
            }
            return t_resolution;
        }
    }
}
