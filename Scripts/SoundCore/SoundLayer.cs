using System.Collections;
using UnityEngine;

namespace NZLib.Utilities.SoundCore
{
    public class SoundLayer
    {
        #region Getters

        public bool IsPlaying => _source.isPlaying;
        public bool IsMuted => _source.mute;
        private MonoBehaviour _getMono
        {
            get
            {
                if (_mono == null)
                {
                    _mono = _source.gameObject.AddComponent<Empty>();
                }
                return _mono;
            }
        }

        #endregion

        /* ===================================================================================== */
        // Parameters

        internal enum Status { Play, Stop, Pause }
        private readonly AudioSource _source;
        private MonoBehaviour _mono;

        /* ===================================================================================== */
        // Base

        public SoundLayer (AudioSource source)
        {
            _source = source;
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Plays a new clip.
        /// </summary>
        /// <param name="clip">AudioClip that will be played.</param>
        /// <param name="volume">Local volume value.</param>
        /// <param name="pitch">Local pitch value.</param>
        /// <param name="loop">Repeats audio clip after it is finished.</param>
        /// <param name="fadeInDuration">If value is greater than 0, it will start a fade in.</param>
        public void Play (AudioClip clip, float volume, float pitch, bool loop, float fadeInDuration)
        {
            _source.clip = clip;
            _source.volume = volume;
            _source.pitch = pitch;
            _source.loop = loop;

            if (fadeInDuration > 0f)
            {
                _source.volume = 0f;
                FadeIn(volume, fadeInDuration);
            }
            _source.Play();
        }

        /// <summary>
        /// Stops playing current source.
        /// </summary>
        /// <param name="fadeOutDuration">If value is greater than 0, it will start a fade out.</param>
        /// <param name="status">Ending source status.</param>
        public void Stop (float fadeOutDuration)
        {
            if (fadeOutDuration > 0f)
            {
                FadeOut(fadeOutDuration, Status.Stop);
                return;
            }
            _source.Stop();
        }

        /// <summary>
        /// Pauses current source.
        /// </summary>
        /// <param name="fadeOutDuration">If value is greater than 0, it will start a fade out.</param>
        public void Pause (float fadeOutDuration)
        {
            if (fadeOutDuration > 0f)
            {
                FadeOut(fadeOutDuration, Status.Pause);
                return;
            }
            _source.Pause();
        }

        /// <summary>
        /// Mutes source.
        /// </summary>
        public void Mute () => _source.mute = true;

        /// <summary>
        /// Unmutes source.
        /// </summary>
        public void Unmute () => _source.mute = false;

        /// <summary>
        /// Processes a fade in.
        /// </summary>
        /// <param name="target">Local volume target value.</param>
        /// <param name="duration">Duration in seconds.</param>
        private void FadeIn (float target, float duration)
        {
            _getMono.StopAllCoroutines();
            _getMono.StartCoroutine(ProcessFade(target, duration, Status.Play));
        }

        /// <summary>
        /// Processes a fade out.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="status">Ending source status.</param>
        private void FadeOut (float duration, Status status)
        {
            _getMono.StopAllCoroutines();
            _getMono.StartCoroutine(ProcessFade(0f, duration, status));
        }

        /// <summary>
        /// Processes a fade in any direction.
        /// </summary>
        /// <param name="target">Local volume target value.</param>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="status">Ending source status.</param>
        private IEnumerator ProcessFade (float target, float duration, Status status)
        {
            var t_time = 0f;
            while (t_time < duration)
            {
                _source.volume = Mathf.Lerp(_source.volume, target, t_time / duration);
                t_time += Time.deltaTime;
                yield return null;
            }
            _source.volume = target;

            if (status == Status.Stop)
            {
                Stop(0f);
            }
            else if (status == Status.Pause)
            {
                Pause(0f);
            }
        }
    }
}
