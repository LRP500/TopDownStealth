using Sirenix.OdinInspector;
using Tool.Extensions;
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
        [BoxGroup("Detection")]
        private FieldOfView _fov = null;
        public FieldOfView FOV => _fov;

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
        [ShowIf(nameof(_path))]
        [PropertyRange(0, "@ _path ? _path.GetWaypointCount() - 1 : 0")]
        [OnValueChanged(nameof(ResetPosition))]
        private int _initialPathPosition = 0;

        private int MaxPathIndex()
        {
            return _path.GetWaypointCount();
        }

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
        }

        protected virtual void Update()
        {
            _behaviour?.Run(this);
        }

        private void Initialize()
        {
            Brain = new CharacterBrain();
            _behaviour?.Initialize(this);
        }

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

        public void Move(Vector3 direction)
        {
            _mover.SetDirection(direction);
        }

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