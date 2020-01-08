using UnityEngine;

namespace TopDownStealth.Characters
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5f;

        [SerializeField]
        private float _rotationSpeed = 20f;
        public float RotationSpeed => _rotationSpeed;

        [SerializeField]
        private float _gravity = 20f;

        private CharacterController _controller = null;

        private Vector3 _direction = Vector3.zero;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        public void SetDirection(Vector3 direction)
        {
            if (_controller.isGrounded)
            {
                _direction = direction;
            }
        }

        private void Update()
        {
            Move();

            _direction = Vector3.zero;
        }

        private void Move()
        {
            _direction.y -= _gravity * Time.deltaTime;
            _controller.Move(_direction * _speed * Time.deltaTime);
        }
    }
}
