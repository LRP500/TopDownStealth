using Tools.Variables;
using UnityEngine;

namespace TopDownShooter
{
    /// <summary>
    /// Handles all player-specific gameplay actions and input.
    /// </summary>
    [CreateAssetMenu(menuName = "Top Down Shooter/Characters/Behaviours/Player Controlled")]
    public class PlayerControlledBehaviour : CharacterBehaviour
    {
        [SerializeField]
        private CameraVariable _mainCamera = null;

        private Character _character = null;

        public override void Initialize(Character character)
        {
        }

        public override void Run(Character character)
        {
            _character = character;

            HandleShooting();
            HandleMovement();
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

        private void HandleShooting()
        {
            Vector3 mousePosition = GetMousePosition();

            _character.LookAt(mousePosition);

            if (Input.GetMouseButton(0))
            {
                _character.UseWeapon(mousePosition);
            }

            if (Input.GetMouseButton(1))
            {
                _character.UseThrowable(mousePosition);
            }
        }

        private void HandleMovement()
        {
            _character.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }
    }
}
