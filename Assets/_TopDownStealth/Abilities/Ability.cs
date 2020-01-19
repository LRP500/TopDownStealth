using Tools.Variables;
using UnityEngine;

namespace TopDownStealth
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField]
        private CharacterVariable _holder = null;
        public CharacterVariable Holder => _holder;

        [SerializeField]
        private FloatVariable _cooldownRatio = null;
        public FloatVariable CooldownRatio => _cooldownRatio;

        public float LastUseTime { get; set; } = 0;

        public bool Active { get; set; } = false;

        protected abstract bool CanActivate();
    }
}