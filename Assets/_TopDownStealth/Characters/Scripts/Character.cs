using Extensions;
using UnityEngine;

namespace TopDownShooter
{
    /// <summary>
    /// Highest-level API for all gameplay related character actions.
    /// </summary>
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        private WeaponModel _initialWeapon = null;

        [SerializeField]
        private ThrowableModel _initialThrowable = null;

        [Space]

        [SerializeField]
        private WeaponController _weapon = null;
        public WeaponController Weapon => _weapon;

        [SerializeField]
        private ThrowableController _throwable = null;
        public ThrowableController Throwable => _throwable;

        [SerializeField]
        private CharacterMovement _mover = null;

        [SerializeField]
        private CharacterStats _stats = null;
        public CharacterStats Stats => _stats;

        [Space]

        [SerializeField]
        private CharacterBehaviour _behaviour = null;

        public Team Team { get; protected set; } = Team.Neutral;

        protected virtual void Awake()
        {
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
            if (_initialWeapon)
            {
                EquipWeapon(_initialWeapon);
            }

            if (_initialThrowable)
            {
                EquipThrowable(_initialThrowable);
            }

            _behaviour?.Initialize(this);
        }

        public void LookAt(Vector3 target)
        {
            if (target != Vector3.zero)
            {
                var position = new Vector3(target.x, transform.position.y, target.z);

                transform.LookAt(position);
                Weapon.LookAt(position);

#if UNITY_EDITOR
                Debug.DrawLine(transform.position, position, Color.green);
#endif
            }
        }

        public WeaponModel SwapWeapon(WeaponModel newWeapon)
        {
            WeaponModel oldWeapon = _weapon.Current.Model;
            EquipWeapon(newWeapon);
            return oldWeapon;
        }

        public ThrowableModel SwapThrowable(ThrowableModel newThrowable)
        {
            ThrowableModel oldThrowable = _throwable.Current.Model;
            EquipThrowable(newThrowable);
            return oldThrowable;
        }

        public void EquipWeapon(WeaponModel weapon)
        {
            _weapon.SetWeapon(weapon);
            _weapon.Current.SetOwner(this);
        }

        public void EquipThrowable(ThrowableModel throwable)
        {
            _throwable.SetCurrent(throwable);
        }

        public void UseWeapon(Vector3 mousePosition)
        {
            _weapon.Fire(this, mousePosition);
        }

        public void UseThrowable(Vector3 target)
        {
            _throwable.Current.TryUse(this, target);
        }

        public void Move(Vector3 direction)
        {
            _mover.SetDirection(direction);
        }

        protected abstract void Die();
    }

    public enum Team
    {
        Ally,
        Enemy,
        Neutral
    }
}