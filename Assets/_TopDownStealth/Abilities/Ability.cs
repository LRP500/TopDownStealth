using System.Collections;
using Tools.Variables;
using TopDownStealth.Characters;
using UnityEngine;

namespace TopDownStealth
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField]
        private Character _holder = null;
        public Character Holder => _holder;

        [SerializeField]
        private FloatVariable _cooldownRatio = null;
        public FloatVariable CooldownRatio => _cooldownRatio;

        [SerializeField]
        private float _powerConsumption = 0;
        public float PowerConsumption => _powerConsumption;

        public float LastUseTime { get; set; } = 0;

        public bool Active { get; set; } = false;

        protected abstract bool CanActivate();
        protected abstract IEnumerator RefreshBehaviourOverTime();
    }
}