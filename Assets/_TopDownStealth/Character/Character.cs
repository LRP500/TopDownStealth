using Sirenix.OdinInspector;
using System.Collections;
using Tool.Extensions;
using Tools.Extensions;
using UnityEngine;

namespace TopDownStealth.Characters
{
    /// <summary>
    /// Highest-level API for all gameplay related character actions.
    /// </summary>
    [RequireComponent(typeof(CharacterMovement))]
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterBehaviour _behaviour = null;

        [SerializeField]
        private FieldOfView _fieldOfView = null;
        public FieldOfView FieldOfView => _fieldOfView;

        [SerializeField]
        [BoxGroup("Navigation")]
        private bool _hasPath = false;

        [SerializeField]
        [BoxGroup("Navigation")]
        [ShowIf(nameof(_hasPath))]
        [OnValueChanged(nameof(ResetPosition))]
        private Path _path = null;
        public Path Path => _path;

        [SerializeField]
        [BoxGroup("Navigation")]
        [ShowIf("@ _hasPath && _path != null")]
        [PropertyRange(0, "@ _path ? _path.GetWaypointCount() - 1 : 0")]
        [OnValueChanged(nameof(ResetPosition))]
        private int _initialPathPosition = 0;

        private CharacterMovement _mover = null;

        public CharacterBrain Brain { get; private set; } = null;

        public CharacterSide Side { get; protected set; } = CharacterSide.Neutral;

        protected virtual void Awake()
        {
            _mover = GetComponent<CharacterMovement>();
        }

        private void Start()
        {
            Initialize();

            StartCoroutine(_behaviour.Run(this));
        }

        protected virtual void Update()
        {
        }

        private void Initialize()
        {
            Brain = new CharacterBrain();
            _behaviour?.Initialize(this);
        }

        #region Movement

        public void LookAt(Vector3 target)
        {
            if (target != Vector3.zero)
            {
                var position = new Vector3(target.x, transform.position.y, target.z);

                transform.LookAt(position);

#if UNITY_EDITOR
                Debug.DrawLine(transform.position, position, Color.green);
#endif
            }
        }

        public IEnumerator RotateTowards(Vector3 target, float duration)
        {
            float elapsed = 0;

            Vector3 direction = (target - transform.position).normalized;
            Quaternion initialRot = transform.rotation;
            Quaternion targetRot = Quaternion.LookRotation(direction);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(initialRot, targetRot, elapsed / duration);
                yield return null;
            }
        }

        public IEnumerator RotateTowards(Vector3 target)
        {
            float angle = float.PositiveInfinity;

            while (!angle.AlmostEqual(0, 0.1f))
            {
                /// Rotate towards target
                Quaternion rotation = Quaternion.LookRotation(target - transform.position);
                float delta = Time.deltaTime * _mover.RotationSpeed;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, delta);

                /// Calculate new angle to target
                Vector3 direction = (target - transform.position).normalized;
                angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);

                yield return null;
            }
        }

        public void Move(Vector3 direction)
        {
            _mover.SetDirection(direction);
        }
    
        #endregion Movement

        protected abstract void Die();

        #region Editor

        /// <summary>
        /// Reset character position on new path set.
        /// </summary>
        private void ResetPosition()
        {
            if (_hasPath && _path)
            {
                if (!_initialPathPosition.InRange(0, _path.GetWaypointCount(), false))
                {
                    _initialPathPosition = 0;
                }

                transform.position = _path.GetWaypointAt(_initialPathPosition);
            }
        }

        #endregion Editor
    }

    public enum CharacterSide
    {
        Neutral,
        Player,
        Enemy
    }
}