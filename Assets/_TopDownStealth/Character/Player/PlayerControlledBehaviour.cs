using System.Collections;
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

        private Player _player = null;

        public override void Initialize(Character character)
        {
        }

        public override IEnumerator Run(Character character)
        {
            _player = character as Player;

            while (character.enabled)
            {
                _player.LookAt(GetMousePosition());

                HandleMovement();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _player.Scanner.Activate();
                }

                yield return null;
            }
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

        private void HandleMovement()
        {
            _player.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }
    }
}
