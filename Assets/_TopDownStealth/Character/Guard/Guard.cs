using Extensions;
using Tools.Extensions;
using UnityEngine;

namespace TopDownStealth.Characters
{
    public class Guard : Character
    {
        [SerializeField]
        private float _detectionTime = 2;
        public float DetectionTime => _detectionTime;

        [SerializeField]
        private float _detectionAngleMultiplier = 2;

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

                if (DetectionLevel > _detectionTime)
                {
                }
            }
            else
            {
                DetectionLevel = 0;
            }
        }

        private float GetDetectionIncrement()
        {
            /// Get player position.
            Vector3 playerPos = FOV.VisibleTargets[0].position;
            playerPos = new Vector3(playerPos.x, 0, playerPos.z);
            
            /// Get direction to player.
            Vector3 dirToPlayer = playerPos - transform.position;
            Debug.DrawLine(transform.position, transform.position + dirToPlayer, Color.magenta, 0.5f);
            
            /// Get angle to player.
            float angleToPlayer = Mathf.Abs(Vector3.Angle(transform.forward, dirToPlayer));
         
            /// Return value to range from 1 to angle multiplier.
            return angleToPlayer.Convert(0, (FOV.Angle / 2), _detectionAngleMultiplier, 1);
        }
    }
}