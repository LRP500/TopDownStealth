using Sirenix.OdinInspector;
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

        [SerializeField]
        private float _detectionInterval = 0.2f;

        [SerializeField]
        private bool _generateMesh = false;

        [SerializeField]
        [ShowIfGroup(nameof(_generateMesh))]
        [BoxGroup(nameof(_generateMesh) + "/Mesh")]
        private MeshFilter _viewMeshFilter = null;

        [SerializeField]
        [BoxGroup(nameof(_generateMesh) + "/Mesh")]
        private float _meshResolution = 0f;

        [SerializeField]
        [BoxGroup(nameof(_generateMesh) + "/Mesh")]
        private int _edgeResolveIterations = 0;

        [SerializeField]
        [BoxGroup(nameof(_generateMesh) + "/Mesh")]
        [LabelText("Edge Distance Threshold")]
        private float _edgeDistThreshold = 0f;

        [SerializeField]
        [BoxGroup(nameof(_generateMesh) + "/Mesh")]
        private float _maskCutawayDistance = 0.1f;

        public List<Transform> VisibleTargets { get; private set; } = null;

        private List<Vector3> _viewPoints = null;

        private Mesh _viewMesh = null;

        private void Awake()
        {
            VisibleTargets = new List<Transform>();
            _viewPoints = new List<Vector3>();

            if (_generateMesh)
            {
                _viewMesh = new Mesh();
                _viewMesh.name = "View Mesh";
                _viewMeshFilter.mesh = _viewMesh;
            }
        }

        private void Start()
        {
            StartCoroutine(FindTargetsWithDelay(_detectionInterval));
        }

        private void LateUpdate()
        {
            if (_generateMesh)
            {
                DrawFieldOfView();
            }
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

        private void DrawFieldOfView()
        {
            int stepCount = Mathf.RoundToInt(_angle * _meshResolution);
            float stepAngleSize = _angle / stepCount;

            _viewPoints.Clear();

            ViewCastInfo oldViewCast = new ViewCastInfo();

            for (int i = 0; i <= stepCount; i++)
            {
                float angle = transform.eulerAngles.y - _angle / 2 + stepAngleSize * i;
                ViewCastInfo newViewCast = ViewCast(angle);

                if (i > 0)
                {
                    bool edgeDistThresholdExceeded = Mathf.Abs(oldViewCast.dist - newViewCast.dist) > _edgeDistThreshold;

                    if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDistThresholdExceeded))
                    {
                        EdgeInfo edge = FindEdge(oldViewCast, newViewCast);

                        if (edge.pointA != Vector3.zero)
                        {
                            _viewPoints.Add(edge.pointA);
                        }

                        if (edge.pointB != Vector3.zero)
                        {
                            _viewPoints.Add(edge.pointB);
                        }
                    }
                }

                _viewPoints.Add(newViewCast.point);
                oldViewCast = newViewCast;
            }

            /// Construct mesh triangles
            int vertexCount = _viewPoints.Count + 1;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector3.zero;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(_viewPoints[i]) + Vector3.forward * _maskCutawayDistance;

                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            _viewMesh.Clear();
            _viewMesh.vertices = vertices;
            _viewMesh.triangles = triangles;
            _viewMesh.RecalculateNormals();
        }

        private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
        {
            float minAngle = minViewCast.angle;
            float maxAngle = maxViewCast.angle;
            Vector3 minPoint = Vector3.zero;
            Vector3 maxPoint = Vector3.zero;

            for (int i = 0; i < _edgeResolveIterations; i++)
            {
                float angle = (minAngle + maxAngle) / 2;
                ViewCastInfo newViewCast = ViewCast(angle);

                bool edgeDistThresholdExceeded = Mathf.Abs(minViewCast.dist - newViewCast.dist) > _edgeDistThreshold;

                if (newViewCast.hit == minViewCast.hit && !edgeDistThresholdExceeded)
                {
                    minAngle = angle;
                    minPoint = newViewCast.point;
                }
                else
                {
                    maxAngle = angle;
                    maxPoint = newViewCast.point;
                }
            }

            return new EdgeInfo(minPoint, maxPoint);
        }

        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        private ViewCastInfo ViewCast(float globalAngle)
        {
            Vector3 dir = DirFromAngle(globalAngle, true);

            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, _radius, _obstacleMask))
            {
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }
            else
            {
                return new ViewCastInfo(false, transform.position + dir * _radius, _radius, globalAngle);
            }
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;

        public ViewCastInfo(bool hit, Vector3 point, float distance, float angle)
        {
            this.hit = hit;
            this.point = point;
            this.dist = distance;
            this.angle = angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 pointA, Vector3 pointB)
        {
            this.pointA = pointA;
            this.pointB = pointB;
        }
    }
}
