using UnityEngine;

namespace NZLib.Utilities.SoundCore
{
    /// <summary>
    /// Contains all Sound properties.
    /// </summary>
    [CreateAssetMenu(fileName = "SoundData", menuName = "SoundCore/SoundData", order = 1)]
    public sealed class SoundData : ScriptableObject
    {
        #region Getters

        public string GetLabel => _label;
        public AudioClip GetClip => _clip;

        #endregion

        /* ===================================================================================== */
        // Parameters

        [SerializeField] private string _label;
        [SerializeField] private AudioClip _clip;
    }
}
