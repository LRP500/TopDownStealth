using Sirenix.OdinInspector;
using Tools;
using Tools.Extensions;
using UnityEngine;

namespace TopDownStealth.Characters
{
    public class Guard : Character
    {
        [SerializeField]
        [BoxGroup("Detection")]
        private float _detectionSpeed = 2;
        public float DetectionSpeed => _detectionSpeed;

        [SerializeField]
        [BoxGroup("Detection")]
        private float _dampeningSpeed = 2;
        public float DampeningSpeed => _dampeningSpeed;

        [SerializeField]
        [BoxGroup("Detection")]
        private float angleExponent = 2;

        [SerializeField]
        [BoxGroup("Detection")]
        private float _distanceExponent = 2;

        public float DetectionLevel { get; private set; } = 0;

        protected override void Awake()
        {
            base.Awake();

            Side = CharacterSide.Enemy;
        }

        protected override void Update()
        {
            base.Update();

            DetectPlayer();
        }

        protected override void Die()
        {
            Destroy(gameObject);
        }

        private void DetectPlayer()
        {
            if (FOV.VisibleTargets.Count > 0)
            {
                DetectionLevel += GetDetectionIncrement() * Time.deltaTime;

                if (DetectionLevel >= 1)
                {
                    /// "Game over man, game over !"
                    ///             - Private Hudson
                    EventManager.Instance.Trigger(GameplayEvent.GameOver);
                    
                    /// Disable script to avoid triggering event multiple times.
                    enabled = false;
                }
            }
            else
            {
                DetectionLevel -= _dampeningSpeed * Time.deltaTime;
                DetectionLevel = Mathf.Clamp(DetectionLevel, 0, _detectionSpeed);
            }

            DetectionLevel = Mathf.Clamp(DetectionLevel, 0, _detectionSpeed);
        }

        private float GetDetectionIncrement()
        {
            /// Get player position.
            Vector3 playerPos = FOV.VisibleTargets[0].position;
            playerPos = new Vector3(playerPos.x, 0, playerPos.z);

            /// Get direction to player.
            Vector3 dirToPlayer = playerPos - transform.position;

            /// Get angle increment.
            float angleToPlayer = Mathf.Abs(Vector3.Angle(transform.forward, dirToPlayer));
            /// Convert to a value between 0 and 1.
            float angleIncrement = angleToPlayer.Convert(0, (FOV.Angle / 2), 1, 0);
            /// Apply angle exponent.
            angleIncrement = Mathf.Pow(angleIncrement, angleExponent);
            /// Multiply by detection speed.
            angleIncrement = angleIncrement * _detectionSpeed;

            /// Get distance increment.
            float distToPlayer = Vector3.Distance(transform.position, playerPos);
            /// Convert to a value between 0 and 1.
            float distIncrement = distToPlayer.Convert(1, FOV.Radius, 1, 0);
            /// Apply angle exponent.
            distIncrement = Mathf.Pow(distIncrement, _distanceExponent);
            distIncrement = distIncrement * _detectionSpeed;

            return angleIncrement + distIncrement;
        }
    }
}