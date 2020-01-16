using System.Collections;
using Tools;
using Tools.Variables;
using UnityEngine;

namespace TopDownStealth.Characters.Behaviours
{
    /// <summary>
    /// Handles all player-specific gameplay actions and input.
    /// </summary>
    [CreateAssetMenu(menuName = "Top Down Stealth/Characters/Behaviours/Player Controlled")]
    public class PlayerControlledBehaviour : CharacterBehaviour
    {
        [SerializeField]
        private CameraVariable _mainCamera = null;

        [SerializeField]
        private bool _rotateTowardsMouse = true;

        private Player _player = null;

        private bool _movementInputEnabled = true;

        public override void Initialize(Character character)
        {
            _movementInputEnabled = true;

            EventManager.Instance.Subscribe(GameplayEvent.EnablePlayerMovementInput, OnEnablePlayerInput);
            EventManager.Instance.Subscribe(GameplayEvent.DisablePlayerMovementInput, OnDisablePlayerInput);
        }

        public override IEnumerator Run(Character character)
        {
            _player = character as Player;

            while (character.enabled)
            {
                HandleRotation();
                HandleMovement();
                HandleAbilities();
                yield return null;
            }
        }

        private void HandleAbilities()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _player.Scanner.Activate();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _player.Camouflage.Activate();
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                _player.Camouflage.Deactivate();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _player.HackingDevice.Activate();
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                _player.HackingDevice.Deactivate();
            }
        }

        private void OnDisablePlayerInput(object arg = null)
        {
            _movementInputEnabled = false;
        }

        private void OnEnablePlayerInput(object arg = null)
        {
            _movementInputEnabled = true;
        }

        public override void Reset(Character character)
        {
            base.Reset(character);

            EventManager.Instance.Unsubscribe(GameplayEvent.EnablePlayerMovementInput, OnEnablePlayerInput);
            EventManager.Instance.Unsubscribe(GameplayEvent.DisablePlayerMovementInput, OnDisablePlayerInput);
        }

        private Vector3 GetMousePosition()
        {
            Ray camerRay = _mainCamera.Value.ScreenPointToRay(Input.mousePosition);
            Plane ground = new Plane(Vector3.up, Vector3.zero);

            if (ground.Raycast(camerRay, out float rayLength))
            {
                return camerRay.GetPoint(rayLength);
            }

            return Vector3.zero;
        }

        private void HandleRotation()
        {
            if (_rotateTowardsMouse)
            {
                _player.LookAt(GetMousePosition());
            }
        }

        private void HandleMovement()
        {
            if (_movementInputEnabled)
            {
                _player.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            }
        }
    }
}
