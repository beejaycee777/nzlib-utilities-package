using UnityEngine;

namespace NZLib.Utilities.AnimationCore
{
    public sealed class AnimatorBoolHandler : AnimatorHandler
    {
        /* ===================================================================================== */
        // Base

        public AnimatorBoolHandler (Animator animator) : base(animator) { }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Activates a bool.
        /// </summary>
        public void Activate (string parameter)
        {
            for (var i = 0; i < _animator.parameterCount; ++i)
            {
                if (_animator.GetParameter(i).name == parameter && _animator.GetParameter(i).type == AnimatorControllerParameterType.Bool)
                {
                    _animator.SetBool(parameter, true);
                }
            }
        }

        /// <summary>
        /// Deactivates all bools before activating the requested one.
        /// </summary>
        public void ActivateOnly (string parameter)
        {
            DeactivateAll();
            Activate(parameter);
        }

        /// <summary>
        /// Deactivates a bool.
        /// </summary>
        public void Deactivate (string parameter)
        {
            for (var i = 0; i < _animator.parameterCount; ++i)
            {
                if (_animator.GetParameter(i).name == parameter && _animator.GetParameter(i).type == AnimatorControllerParameterType.Bool)
                {
                    _animator.SetBool(parameter, false);
                }
            }
        }

        /// <summary>
        /// Deactivate all bools.
        /// </summary>
        public void DeactivateAll ()
        {
            for (var i = 0; i < _animator.parameterCount; ++i)
            {
                if (_animator.GetParameter(i).type == AnimatorControllerParameterType.Bool)
                {
                    _animator.SetBool(_animator.GetParameter(i).name, false);
                }
            }
        }
    }
}
