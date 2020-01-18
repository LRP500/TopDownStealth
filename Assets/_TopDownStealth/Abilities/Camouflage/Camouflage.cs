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
    }
}