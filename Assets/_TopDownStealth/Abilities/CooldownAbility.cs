using System.Collections;
using Tools.Extensions;
using UnityEngine;

namespace TopDownStealth
{
    public abstract class CooldownAbility : Ability
    {
        [SerializeField]
        private float _cooldownTime = 1f;
        public float CooldownTime => _cooldownTime;

        [SerializeField]
        private float _powerConsumption = 10f;

        protected virtual void Awake()
        {
            LastUseTime = float.NegativeInfinity;
        }

        private void Update()
        {
            /// Cooldown
            float timeSinceLastUse = Time.time - LastUseTime;
            timeSinceLastUse = Mathf.Clamp(timeSinceLastUse, 0, CooldownTime);
            CooldownRatio.SetValue(timeSinceLastUse.Convert(0, CooldownTime, 0, 1));
        }

        public bool Activate()
        {
            if (CanActivate())
            {
                LastUseTime = Time.time;
                StartCoroutine(Refresh());
            }

            return true;
        }

        protected override bool CanActivate()
        {
            return CooldownRatio >= 1;
        }

        protected abstract IEnumerator Refresh();
    }
}
