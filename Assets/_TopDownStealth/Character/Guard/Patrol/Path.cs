using Sirenix.OdinInspector;
using UnityEngine;

namespace TopDownStealth.Characters
{
    public class Path : MonoBehaviour
    {
        private Vector3[] _waypoints = null;

        public Vector3[] Waypoints
        {
            get
            {
                _waypoints = _waypoints ?? GetWaypoints();
                return _waypoints;
            }
        }

        private Vector3[] GetWaypoints()
        {
            Vector3[] waypoints = new Vector3[transform.childCount];

            for (int i = 0, length = waypoints.Length; i < length; i++)
            {
                waypoints[i] = transform.GetChild(i).transform.position;
            }

            return waypoints;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Vector3[] waypoints = GetWaypoints();
            Vector3 previousPosition = waypoints[0];

            foreach (Vector3 waypoint in waypoints)
            {
                Gizmos.DrawWireCube(waypoint, Vector3.one / 2);
                Gizmos.DrawLine(previousPosition, waypoint);
                previousPosition = waypoint;
            }

            Gizmos.DrawLine(previousPosition, waypoints[0]);
        }
    }
}
