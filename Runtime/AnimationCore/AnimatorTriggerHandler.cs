using UnityEngine;

namespace NZLib.Utilities.AnimationCore
{
    public sealed class AnimatorTriggerHandler : AnimatorHandler
    {
        /* ===================================================================================== */
        // Base

        public AnimatorTriggerHandler (Animator animator) : base(animator) { }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Launches a trigger.
        /// </summary>
        public void Launch (string trigger)
        {
            ResetAll();
            for (var i = 0; i < _animator.parameterCount; ++i)
            {
                if (_animator.GetParameter(i).name == trigger && _animator.GetParameter(i).type == AnimatorControllerParameterType.Trigger)
                {
                    _animator.SetTrigger(trigger);
                }
            }
        }

        /// <summary>
        /// Resets a trigger.
        /// </summary>
        public void Reset (string trigger)
        {
            for (var i = 0; i < _animator.parameterCount; ++i)
            {
                if (_animator.GetParameter(i).name == trigger && _animator.GetParameter(i).type == AnimatorControllerParameterType.Trigger)
                {
                    _animator.ResetTrigger(trigger);
                }
            }
        }

        /// <summary>
        /// Resets all triggers.
        /// </summary>
        public void ResetAll ()
        {
            for (var i = 0; i < _animator.parameterCount; ++i)
            {
                if (_animator.GetParameter(i).type == AnimatorControllerParameterType.Trigger)
                {
                    _animator.ResetTrigger(_animator.GetParameter(i).name);
                }
            }
        }
    }
}
