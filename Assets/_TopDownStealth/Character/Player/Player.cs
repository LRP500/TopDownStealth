using UnityEngine;

namespace TopDownStealth.Characters
{
    public class Player : Character
    {
        [SerializeField]
        private Scanner _scanner = null;
        public Scanner Scanner => _scanner;

        [SerializeField]
        private CharacterVariable _runtimeReference = null;

        protected override void Awake()
        {
            base.Awake();

            Side = CharacterSide.Player;

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
