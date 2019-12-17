using UnityEngine;

namespace TopDownShooter
{
    public class Player : Character
    {
        [Space]
        [SerializeField]
        private CharacterVariable _runtimeReference = null;

        protected override void Awake()
        {
            base.Awake();

            Team = Team.Ally;

            _runtimeReference.SetValue(this);
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Die()
        {
        }
    }
}
