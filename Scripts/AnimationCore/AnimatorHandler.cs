using UnityEngine;

namespace NZLib.Utilities.AnimationCore
{
    /// <summary>
    /// Manages an Animator parameter.
    /// </summary>
    public abstract class AnimatorHandler
    {
        /* ===================================================================================== */
        // Components

        /// <summary>
        /// Animator component.
        /// </summary>
        protected readonly Animator _animator;

        /* ===================================================================================== */
        // Base

        public AnimatorHandler (Animator animator)
        {
            _animator = animator;
        }
    }
}
