using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace NZLib.Utilities.SoundCore
{
    /// <summary>
    /// Controls all sounds.
    /// </summary>
    public sealed class SoundController
    {
        #region Singleton

        private static readonly SoundController _instance = new SoundController();
        public static SoundController Instance => _instance;

        #endregion

        #region Events

        public UnityAction OnPlayBGM;
        public UnityAction OnPlaySFX;

        #endregion

        #region Event handlers

        private void HandleOnSceneLoaded (Scene scene, LoadSceneMode mode)
        {
            _bgmLayer = new List<SoundLayer>();
            _sfxLayer = new List<SoundLayer>();
        }

        #endregion

        #region Getters

        /// <summary>
        /// Returns a library based on requested layer.
        /// </summary>
        /// <param name="layer">Library layer.</param>
        private Dictionary<string, AudioClip> _getLibrary (Layer layer)
        {
            if (layer == Layer.BGM)
            {
                return _bgmLibrary;
            }
            else
            {
                return _sfxLibrary;
            }
        }
        /// <summary>
        /// Returns a layer list based on requested layer.
        /// </summary>
        /// <param name="layer">Layer list type</param>
        private List<SoundLayer> _getLayerList (Layer layer)
        {
            if (layer == Layer.BGM)
            {
                return _bgmLayer;
            }
            else
            {
                return _sfxLayer;
            }
        }

        #endregion

        public enum Layer { BGM, SFX };

        /* ===================================================================================== */
        // Components

        /// <summary>
        /// Master volume for BGM.
        /// </summary>
        public readonly Volume MasterVolumeBGM = new Volume();
        /// <summary>
        /// Master volume for SFX.
        /// </summary>
        public readonly Volume MasterVolumeSFX = new Volume();
        /// <summary>
        /// Full BGM library found in 'Resources/Sound/BGM'.
        /// </summary>
        private readonly Dictionary<string, AudioClip> _bgmLibrary = new Dictionary<string, AudioClip>();
        /// <summary>
        /// Full SFX library found in 'Resources/Sound/SFX'.
        /// </summary>
        private readonly Dictionary<string, AudioClip> _sfxLibrary = new Dictionary<string, AudioClip>();
        /// <summary>
        /// BGM layer list.
        /// </summary>
        private List<SoundLayer> _bgmLayer = new List<SoundLayer>();
        /// <summary>
        /// SFX layer list.
        /// </summary>
        private List<SoundLayer> _sfxLayer = new List<SoundLayer>();        

        /* ===================================================================================== */
        // Base

        private SoundController ()
        {
            SceneManager.sceneLoaded += HandleOnSceneLoaded;
            // Generates BGM library.
            var t_bgmList = Resources.LoadAll<SoundData>("Sound/BGM");
            for (var i = 0; i <  t_bgmList.Length; i++)
            {
                _bgmLibrary.Add(t_bgmList[i].GetLabel, t_bgmList[i].GetClip);
            }
            // Generates SFX library.
            var t_sfxList = Resources.LoadAll<SoundData>("Sound/SFX");
            for (var i = 0; i < t_sfxList.Length; i++)
            {
                _sfxLibrary.Add(t_sfxList[i].GetLabel, t_sfxList[i].GetClip);
            }
        }

        ~SoundController ()
        {
            SceneManager.sceneLoaded -= HandleOnSceneLoaded;
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Plays requested track.
        /// </summary>
        /// <param name="layer">Library layer.</param>
        /// /// <param name="track">Track label.</param>
        public void Play (Layer layer, string track) => Play(layer, track, 1f, 1f, false, 0f);
        /// <summary>
        /// Plays requested track.
        /// </summary>
        /// <param name="layer">Library layer.</param>
        /// <param name="track">Track label.</param>
        /// <param name="volume">Local volume value.</param>
        public void Play (Layer layer, string track, float volume) => Play(layer, track, volume, 1f, false, 0f);
        /// <summary>
        /// Plays requested track.
        /// </summary>
        /// <param name="layer">Library layer.</param>
        /// <param name="track">Track label.</param>
        /// <param name="volume">Local volume value.</param>
        /// <param name="pitch">Local pitch value.</param>
        /// <param name="loop">Repeats audio clip after it is finished.</param>
        /// <param name="fadeDuration">If value is greater than 0, it will start a fade in.</param>
        public void Play (Layer layer, string track, float volume, float pitch, bool loop, float fadeDuration)
        {
            if (!_getLibrary(layer).ContainsKey(track))
            {
                Debug.LogWarning("Could not find any BGM track with label '" + track + "'.");
                return;
            }
            var t_clip = _getLibrary(layer)[track];
            for (var i = 0; i < _getLayerList(layer).Count; i++)
            {
                if (!_getLayerList(layer)[i].IsPlaying)
                {
                    _getLayerList(layer)[i].Play(t_clip, volume * MasterVolumeBGM.Value, pitch, loop, fadeDuration);
                    OnPlayBGM?.Invoke();
                    return;
                }
            }
            // If there is not any layer ready, it creates a whole new one.
            AddNewLayer(layer);
            Play(layer, track, volume, pitch, loop, fadeDuration);
        }

        /// <summary>
        /// Stops requested sound layer.
        /// </summary>
        /// <param name="layer">Sound layer type.</param>
        public void Stop (Layer layer) => Stop(layer, 0f);
        /// <summary>
        /// Stops requested sound layer.
        /// </summary>
        /// <param name="layer">Sound layer type.</param>
        /// <param name="fadeDuration">If value is greater than 0, it will start a fade out.</param>
        public void Stop (Layer layer, float fadeDuration)
        {
            for (var i = 0; i < _getLayerList(layer).Count; ++i)
            {
                _getLayerList(layer)[i].Stop(fadeDuration);
            }
        }

        /// <summary>
        /// Pauses requested sound layer.
        /// </summary>
        /// <param name="layer">Sound layer type.</param>
        public void Pause (Layer layer) => Pause(layer, 0f);
        /// <summary>
        /// Pauses requested sound layer.
        /// </summary>
        /// <param name="layer">Sound layer type.</param>
        /// <param name="fadeDuration">If value is greater than 0, it will start a fade out.</param>
        public void Pause (Layer layer, float fadeDuration)
        {
            for (var i = 0; i < _getLayerList(layer).Count; ++i)
            {
                _getLayerList(layer)[i].Pause(fadeDuration);
            }
        }

        /// <summary>
        /// Adds a new sound layer in list.
        /// </summary>
        /// <param name="layer">Layer list type.</param>
        private void AddNewLayer (Layer layer)
        {
            var t_source = new GameObject(layer + "_Layer_" + _getLayerList(layer).Count).AddComponent<AudioSource>();
            _getLayerList(layer).Add(new SoundLayer(t_source));
        }
    }
}
