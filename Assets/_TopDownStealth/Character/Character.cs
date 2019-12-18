using UnityEngine;

namespace TopDownStealth
{
    /// <summary>
    /// Highest-level API for all gameplay related character actions.
    /// </summary>
    [RequireComponent(typeof(CharacterMovement))]
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterBehaviour _behaviour = null;

        private CharacterMovement _mover = null;

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
    }

    public enum CharacterSide
    {
        Neutral,
        Player,
        Enemy
    }
}