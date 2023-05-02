using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NZLib.Utilities.InputCore
{
    /// <summary>
    /// Manages Keyboard behaviour.
    /// </summary>
    public sealed class Keyboard : MonoBehaviour
    {
        [System.Serializable]
        public class KeyCodeProperty
        {
            #region Getters

            public KeyCode GetKeyCode => _key;
            public KeyCode GetModifier => _modifier;
            public UnityEvent GetAction => _action;

            #endregion

            [SerializeField] private KeyCode _key = KeyCode.Return;
            [SerializeField] private KeyCode _modifier = KeyCode.None;
            [SerializeField] private UnityEvent _action = new();
        }

        #region Events

        public static UnityAction<KeyCode> OnPressKey;

        #endregion

        #region Getters

        public bool IsEnabled => _enabled;

        #endregion

        /* ===================================================================================== */
        // Parameters

        /// <summary>
        /// Returns current status.
        /// </summary>
        [SerializeField] private bool _enabled = true;
        /// <summary>
        /// Time to wait between every key press.
        /// </summary>
        [SerializeField] private float _inputLag = 0f;
        /// <summary>
        /// Contains every Key associated to its Action.
        /// </summary>
        [SerializeField] private List<KeyCodeProperty> _keyCodemap = new();

        /* ===================================================================================== */
        // Base

        private void Start ()
        {
            var t_keyboards = GameObject.FindObjectsOfType<Keyboard>();
            if (t_keyboards.Length > 1)
            {
                Debug.LogError("There cannot be more than 1 'Keyboard' in scene. Please, destroy all unnecessary instances.");
            }
        }

        private void Update ()
        {
            if (_enabled)
            {
                if (_keyCodemap.Count > 0)
                {
                    CheckInput();
                }
            }
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Adds a new action to a KeyProperty.
        /// </summary>
        /// <param name="key">KeyCode assigned.</param>
        /// <param name="modifier">Modifier assigned.</param>
        /// <param name="action">Action to be assigned.</param>
        public void AddAction (KeyCode key, KeyCode modifier, UnityAction action)
        {
            for (var i = 0; i < _keyCodemap.Count; i++)
            {
                if (_keyCodemap[i].GetKeyCode == key && _keyCodemap[i].GetModifier == modifier)
                {
                    _keyCodemap[i].GetAction.AddListener(action);
                    return;
                }
            }
            Debug.LogWarning("Could not find any 'KeyProperty' with key '" + key + "' and modifier '" + modifier + "'.");
        }

        /// <summary>
        /// Enables behaviour.
        /// </summary>
        public void Enable ()
        {
            _enabled = true;
            Debug.LogWarning("'Keyboard' has been enabled.");
        }

        /// <summary>
        /// Disables behaviour.
        /// </summary>
        public void Disable ()
        {
            _enabled = false;
            Debug.LogWarning("'Keyboard' has been disabled.");
        }

        /// <summary>
        /// Example method to print something on console.
        /// </summary>
        /// <param name="text">Text to print.</param>
        public void PrintExample (string text)
        {
            Debug.Log(text);
        }

        /// <summary>
        /// Checks for input keys.
        /// </summary>
        private void CheckInput ()
        {
            for (var i = 0; i < _keyCodemap.Count; ++i)
            {
                if (_keyCodemap[i].GetModifier != KeyCode.None)
                {
                    if (Input.GetKey(_keyCodemap[i].GetModifier))
                    {
                        for (var n = 0; n < _keyCodemap.Count; n++)
                        {
                            if (_keyCodemap[n].GetModifier == _keyCodemap[i].GetModifier)
                            {
                                if (Input.GetKeyDown(_keyCodemap[n].GetKeyCode))
                                {
                                    HandleOnPressKey(_keyCodemap[n]);
                                    return;
                                }
                            }
                        }
                        return;
                    }
                }                
                else if (Input.GetKeyDown(_keyCodemap[i].GetKeyCode))
                {
                    HandleOnPressKey(_keyCodemap[i]);
                }
            }
        }

        /// <summary>
        /// Processes a new input.
        /// </summary>
        /// <param name="code">Key code that has been input.</param>
        /// <param name="newEvent">Event(s) related to given key code.</param>
        private void HandleOnPressKey (KeyCodeProperty property)
        {
            OnPressKey?.Invoke(property.GetKeyCode);
            property.GetAction?.Invoke();
            if (_inputLag > 0f)
            {
                StartCoroutine(GenerateInputLag());
            }
        }

        /// <summary>
        /// Disables behaviour temporary between every key press.
        /// </summary>
        private IEnumerator GenerateInputLag ()
        {
            Disable();
            yield return new WaitForSeconds(_inputLag);
            Enable();
        }
    }
}
