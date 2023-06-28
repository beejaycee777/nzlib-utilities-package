using System.Collections;
using UnityEngine;

namespace NZLib.Utilities.UI
{
    public sealed class MovingObject2D : MonoBehaviour
    {
        [System.Serializable]
        internal class MovingProperty
        {
            /// <summary>
            /// Returns TRUE if property is active.
            /// </summary>
            public bool Active = true;
            /// <summary>
            /// Moving speed.
            /// </summary>
            public float Speed = 0.05f;
            /// <summary>
            /// Minimum duration in seconds.
            /// </summary>
            [Range(0f, 10f)] public float DurationMin = 1.5f;
            /// <summary>
            /// Maximum duration in seconds.
            /// </summary>
            [Range(0f, 10f)] public float DurationMax = 3.5f;
            /// <summary>
            /// Position limits will use the entire vector: -x, x, -y, y.
            /// Rotation limits will use only x and y: x, y, 0, 0.
            /// </summary>
            [HideInInspector] public Vector4 Limit = Vector4.zero;
        }

        /* ===================================================================================== */
        // Parameters        

        [Header("Position")]
        /// <summary>
        /// Position moving property.
        /// </summary>
        [SerializeField] private MovingProperty _position = new();
        /// <summary>
        /// Maximum reachable distance from current point to any direction.
        /// </summary>
        [SerializeField][Range(0f, 100)] private float _distanceMax = 7.5f;
        /// <summary>
        /// Initial position.
        /// </summary>
        [SerializeField] private Vector2 _initialPosition = Vector2.zero;

        [Header("Rotation")]
        /// <summary>
        /// Rotation moving property.
        /// </summary>
        [SerializeField] private MovingProperty _rotation = new();
        /// <summary>
        /// Maximum reachable local rotation.
        /// </summary>
        [SerializeField][Range(0f, 180f)] private float _rotationMax = 15f;
        
        /// <summary>
        /// Allows moving and/or rotating.
        /// </summary>
        private bool _active = true;

        /* ===================================================================================== */
        // Base

        private void Start ()
        {
            _position.Limit.x = _initialPosition.x - _distanceMax;
            _position.Limit.y = _initialPosition.x + _distanceMax;
            _position.Limit.z = _initialPosition.y - _distanceMax;
            _position.Limit.w = _initialPosition.y + _distanceMax;
            _rotation.Limit.x = 360f - _rotationMax;
            _rotation.Limit.y = _rotationMax;

            if (_position.Active)
            {
                StartCoroutine(Move());
            }
            if (_rotation.Active)
            {
                StartCoroutine(Rotate());
            }
        }

        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Activates Move and Rotate.
        /// </summary>
        public void Activate ()
        {
            _active = true;
            if (_position.Active)
            {
                StartCoroutine(Move());
            }
            if (_rotation.Active)
            {
                StartCoroutine(Rotate());
            }
        }

        /// <summary>
        /// Inactivates Move and Rotate.
        /// </summary>
        public void Inactivate ()
        {
            _active = false;
        }

        /// <summary>
        /// Moves randomly.
        /// </summary>
        private IEnumerator Move ()
        {
            var t_target = new Vector2(Random.Range(-_position.Speed, _position.Speed), Random.Range(-_position.Speed, _position.Speed));
            var t_time = 0f;
            var t_duration = Random.Range(_position.DurationMin, _position.DurationMax);
            while (t_time < t_duration)
            {
                transform.Translate(t_target);
                t_time += Time.deltaTime;
                if (ReachedPositionLimit())
                {
                    t_time = t_duration;
                }
                yield return null;
            }
            if (_active)
            {
                CoroutineManager.Restart(this, Move());
            }
        }

        /// <summary>
        /// Rotates randomly.
        /// </summary>
        private IEnumerator Rotate ()
        {
            var t_target = new Vector3(0, 0, _rotation.Speed * Random.Range(-1, 2));
            var t_time = 0f;
            var t_duration = Random.Range(_rotation.DurationMin, _rotation.DurationMax);
            while (t_time < t_duration)
            {
                transform.Rotate(t_target);
                t_time += Time.deltaTime;
                if (ReachedRotationLimit())
                {
                    t_time = t_duration;
                }
                yield return null;
            }
            if (_active)
            {
                CoroutineManager.Restart(this, Rotate());
            }
        }

        /// <summary>
        /// Returns TRUE if it has reached any position limit.
        /// </summary>
        private bool ReachedPositionLimit ()
        {
            var t_position = new Vector2(transform.localPosition.x, transform.localPosition.y);

            if (t_position.x < _position.Limit.x)
            {
                t_position.x = _position.Limit.x;
            }
            else if (t_position.x > _position.Limit.y)
            {
                t_position.x = _position.Limit.y;
            }

            if (t_position.y < _position.Limit.z)
            {
                t_position.y = _position.Limit.z;
            }
            else if (t_position.y > _position.Limit.w)
            {
                t_position.y = _position.Limit.w;
            }

            if ((Vector2)transform.localPosition != t_position)
            {
                transform.localPosition = t_position;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns TRUE if it has reached any rotation limit.
        /// </summary>
        private bool ReachedRotationLimit ()
        {
            var t_rotation = transform.localRotation;
            if (t_rotation.eulerAngles.z > _rotation.Limit.y && t_rotation.eulerAngles.z < _rotation.Limit.x)
            {
                if (Mathf.Abs(t_rotation.eulerAngles.z - _rotation.Limit.x) < Mathf.Abs(t_rotation.eulerAngles.z - _rotation.Limit.y))
                {
                    t_rotation = Quaternion.Euler(0f, 0f, _rotation.Limit.x);
                }
                else
                {
                    t_rotation = Quaternion.Euler(0f, 0f, _rotation.Limit.y);
                }
            }

            if (transform.localRotation != t_rotation)
            {
                transform.localRotation = t_rotation;
                return true;
            }
            return false;
        }
    }
}
