using Sirenix.OdinInspector;
using UnityEngine;

namespace TopDownStealth.Characters
{
    public class Player : Character
    {
        [SerializeField]
        [BoxGroup("Resources")]
        private PowerManager _power = null;
        public PowerManager Power => _power;

        [SerializeField]
        [BoxGroup("Abilities")]
        private Scanner _scanner = null;
        public Scanner Scanner => _scanner;

        [SerializeField]
        [BoxGroup("Abilities")]
        private Camouflage _camouflage = null;
        public Camouflage Camouflage => _camouflage;

        [SerializeField]
        [BoxGroup("Abilities")]
        private HackingDevice _hackingDevice = null;
        public HackingDevice HackingDevice => _hackingDevice;

        [SerializeField]
        [BoxGroup("Utilities")]
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
