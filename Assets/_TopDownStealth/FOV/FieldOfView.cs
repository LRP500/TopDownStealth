using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownStealth
{
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField]
        private float _radius = 1f;
        public float Radius => _radius;

        [Range(0, 360)]
        [SerializeField]
        private float _angle = 90f;
        public float Angle => _angle;

        [SerializeField]
        private LayerMask _targetMask = default;

        [SerializeField]
        private LayerMask _obstacleMask = default;

        public List<Transform> VisibleTargets { get; private set; } = null;

        private void Awake()
        {
            VisibleTargets = new List<Transform>();
        }

        private void Start()
        {
            StartCoroutine(FindTargetsWithDelay(0.2f));
        }

        private IEnumerator FindTargetsWithDelay(float delay)
        {
            while (enabled)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            }
        }

        private void FindVisibleTargets()
        {
            VisibleTargets.Clear();

            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _radius, _targetMask);

            for (int i = 0, length = targetsInViewRadius.Length; i < length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, dirToTarget) < _angle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, _obstacleMask))
                    {
                        VisibleTargets.Add(target);
                    }
                }
            }
        }

        public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
