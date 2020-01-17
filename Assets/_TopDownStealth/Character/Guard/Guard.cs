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

        [BoxGroup("Utilities")]
        [SerializeField, Required]
        private CharacterListVariable _runtimeGuards = null;

        [SerializeField]
        [BoxGroup("Character")]
        private Hackable _hackable = null;

        public float DetectionLevel { get; private set; } = 0;

        protected override void Awake()
        {
            base.Awake();

            Initialize();
        }

        private void Initialize()
        {
            _runtimeGuards.Add(this);
            Side = CharacterSide.Enemy;
            _hackable.SubscribeOnHackSuccessful(Disable);
            _hackable.SubscribeOnHackTimedOut(Enable);
        }

        protected override void Update()
        {
            base.Update();

            DetectPlayer();
        }

        private void OnDestroy()
        {
            _hackable.UnsubscribeOnHackSuccessful(Disable);
            _hackable.UnsubscribeOnHackTimedOut(Enable);
            _runtimeGuards.Remove(this);
        }

        protected override void Die()
        {
            Destroy(gameObject);
        }

        private void DetectPlayer()
        {
            if (FieldOfView.VisibleTargets.Count == 0 ||
                FieldOfView.VisibleTargets[0].GetComponent<Detectable>().IsDetectable == false)
            {
                DetectionLevel -= _dampeningSpeed * Time.deltaTime;
                DetectionLevel = Mathf.Clamp(DetectionLevel, 0, _detectionSpeed);
            }
            else
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

            DetectionLevel = Mathf.Clamp(DetectionLevel, 0, _detectionSpeed);
        }

        private float GetDetectionIncrement()
        {
            /// Get player position.
            Vector3 playerPos = FieldOfView.VisibleTargets[0].position;
            playerPos = new Vector3(playerPos.x, 0, playerPos.z);

            /// Get direction to player.
            Vector3 dirToPlayer = playerPos - transform.position;

            /// Get angle increment.
            float angleToPlayer = Mathf.Abs(Vector3.Angle(transform.forward, dirToPlayer));
            /// Convert to a value between 0 and 1.
            float angleIncrement = angleToPlayer.Convert(0, (FieldOfView.Angle / 2), 1, 0);
            /// Apply angle exponent.
            angleIncrement = Mathf.Pow(angleIncrement, angleExponent);
            /// Multiply by detection speed.
            angleIncrement = angleIncrement * _detectionSpeed;

            /// Get distance increment.
            float distToPlayer = Vector3.Distance(transform.position, playerPos);
            /// Convert to a value between 0 and 1.
            float distIncrement = distToPlayer.Convert(1, FieldOfView.Radius, 1, 0);
            /// Apply angle exponent.
            distIncrement = Mathf.Pow(distIncrement, _distanceExponent);
            distIncrement = distIncrement * _detectionSpeed;

            return angleIncrement + distIncrement;
        }

        private void Disable()
        {
            DetectionLevel = 0;
            enabled = false;
        }

        private void Enable()
        {
            enabled = true;
        }
    }
}