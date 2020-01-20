using System.Collections;
using Tools;
using TopDownStealth.Characters;
using UnityEngine;

namespace TopDownStealth
{
    public class Camouflage : ToggleAbility
    {
        [SerializeField]
        private Player _player = null;

        [SerializeField]
        private Material _playerMaterial = null;

        public override void Activate()
        {
            base.Activate();

            if (Active)
            {
                _player.Detectable.SetDetectable(false);
                _playerMaterial.EnableKeyword("ENABLE_CAMOUFLAGE");
                EventManager.Instance.Trigger(GameplayEvent.DisablePlayerMovementInput);

                StartCoroutine(RefreshBehaviourOverTime());
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();

            if (!Active)
            {
                _player.Detectable.SetDetectable(true);
                _playerMaterial.DisableKeyword("ENABLE_CAMOUFLAGE");
                EventManager.Instance.Trigger(GameplayEvent.EnablePlayerMovementInput);
            }
        }

        protected override bool CanActivate()
        {
            return Holder.Power.Current >= 0;
        }

        protected override IEnumerator RefreshBehaviourOverTime()
        {
            while (Active)
            {
                Holder.Power.Substract(PowerConsumption * Time.deltaTime);
                
                if (Holder.Power.Current <= 0)
                {
                    Deactivate();
                    yield break;
                }

                yield return null;
            }
        }
    }
}